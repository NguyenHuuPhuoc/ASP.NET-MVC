using DOMAIN.Models;
using MvcPaging;

namespace ShopManager.Models
{
    public class DesignViewModel
    {
        public string KeyWord { get; set; }

        public int? ParentCategoryId { get; set; }

        public IPagedList<Design> Designs { get; set; }
    }

    public class DesignModelDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreateDate { get; set; }

        public string CreateBy { get; set; }

        public string UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public string Descreption { get; set; }

        public string Status { get; set; }

        public string ParentCategory { get; set; }
    }
}