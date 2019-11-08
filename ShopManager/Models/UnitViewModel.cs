using DOMAIN.Models;
using MvcPaging;

namespace ShopManager.Models
{
    public class UnitViewModel
    {
        public string KeyWord { get; set; }

        public IPagedList<Unit> Units { get; set; }
    }

    public class UnitModelDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreateDate { get; set; }

        public string CreateBy { get; set; }

        public string UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public string Descreption { get; set; }

        public string Status { get; set; }
    }
}