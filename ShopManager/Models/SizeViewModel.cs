using DOMAIN.Models;
using MvcPaging;

namespace ShopManager.Models
{
    public class SizeViewModel
    {
        public double? Width { get; set; }

        public double? Height { get; set; }

        public string Unit { get; set; }

        public IPagedList<Size> Sizes { get; set; }
    }
}