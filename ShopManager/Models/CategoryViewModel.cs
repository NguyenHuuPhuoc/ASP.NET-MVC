using DOMAIN.Models;
using MvcPaging;
using System.Collections.Generic;

namespace ShopManager.Models
{
    public class CategoryViewModel
    {
        public string KeyWord { get; set; }

        public int? ParentId { get; set; }

        public int? TypeId { get; set; }

        public IEnumerable<Category> Parents { get; set; }

        public IPagedList<Category> Categories { get; set; }
    }

    public class CategoryModelDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string CreateDate { get; set; }

        public string CreateBy { get; set; }

        public string UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public string Parent { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Descreption { get; set; }
    }
}