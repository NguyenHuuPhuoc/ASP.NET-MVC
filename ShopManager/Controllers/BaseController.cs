using ContextConnection.ContextDB;
using DOMAIN.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SERVICE.IService;
using ShopManager.App_Start;
using System;
using System.Web;
using System.Web.Mvc;

namespace ShopManager.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IErrorService _errorService;

        public BaseController(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected ApplicationUser GetCurrentInstance()
        {
            string currentUserName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            return UserManager.FindByName(currentUserName);
        }

        protected void Log(Exception ex)
        {
            Error model = new Error();
            model.Message = ex.Message;
            model.Stacktrace = ex.StackTrace;
            model.CreateDate = DateTime.Now;
            model.Status = false;
            _errorService.CreateNew(model);
            _errorService.CommitChanges();
        }

        protected void AlertSuccess(string message)
        {
            TempData["toastrType"] = "success";
            TempData["toastrMessage"] = message;
        }

        protected void AlertInfo(string message)
        {
            TempData["toastrType"] = "info";
            TempData["toastrMessage"] = message;
        }

        protected void AlertWarning(string message)
        {
            TempData["toastrType"] = "warning";
            TempData["toastrMessage"] = message;
        }

        protected void AlertError(string message)
        {
            TempData["toastrType"] = "error";
            TempData["toastrMessage"] = message;
        }

        public void ClearAlert()
        {
            TempData["toastrType"] = null;
            TempData["toastrMessage"] = null;
        }
    }
}