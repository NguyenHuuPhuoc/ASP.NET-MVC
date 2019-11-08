using DOMAIN.Models;
using MvcPaging;

namespace ShopManager.Models
{
    public class RoleViewModel
    {
        public string KeyWord { get; set; }

        public IPagedList<Role> AppRoles { get; set; }
    }

    public class RolesModelDetail
    {
        public int Id { get; set; }

        public string Descreption { get; set; }

        public bool IsCheck { get; set; }
    }
}