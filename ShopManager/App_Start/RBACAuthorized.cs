using ASPNetIdentityManagment.IService;
using DOMAIN.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManager.App_Start
{
    public class RBACAuthorized : AuthorizeAttribute
    {
        private ApplicationUserManager _userManager;

        public RBACAuthorized(): base()
        {
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userIdentity = HttpContext.Current.GetOwinContext().Authentication.User;
            var currentUser = UserManager.FindByName(userIdentity.Identity.Name);
            var groupsOfUser = ServiceFactory.Get<IGroupService>().GetGroupIdsByUserId(currentUser.Id);
            var lstRoles = new List<Role>();
            foreach (var group in groupsOfUser)
            {
                var roleIds = ServiceFactory.Get<IRoleService>().GetRolesByGroupId(group.GroupId);
                foreach (var r in roleIds)
                {
                    var role = ServiceFactory.Get<IRoleService>().GetByKey(r.RoleId);
                    lstRoles.Add(role);
                }
            }

            var result = lstRoles.Distinct();

            if (result.Any(n => n.Name.Equals(this.Roles)))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/NonAutithencation.cshtml"
            };
        }
    }
}