using DOMAIN.Models;
using MvcPaging;

namespace ShopManager.Models
{
    public class MeterialViewModel
    {
        public string KeyWord { get; set; }

        public IPagedList<Meterial> Meterials { get; set; }
    }

    public class MeterialModelDetail
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