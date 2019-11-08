using System.Collections.Generic;

namespace COMMON.Helper
{
    public static class TypeCategory
    {
        public static IEnumerable<CategoryTypeModelDetail> GetTypes()
        {
            var lst = new List<CategoryTypeModelDetail>()
            {
                new CategoryTypeModelDetail { TypeId = 1, Name = "Danh mục dành cho các loại sản phẩm" },
                new CategoryTypeModelDetail { TypeId = 2, Name = "Danh mục dành cho các loại tin tức" }
            };

            return lst;
        }
    }

    public class CategoryTypeModelDetail
    {
        public int TypeId { get; set; }

        public string Name { get; set; }
    }

    public static class UnitOfSizeCategory
    {
        public static IEnumerable<UnitOfSizeModelDetail> GetUnitsOfSize()
        {
            return new List<UnitOfSizeModelDetail>()
            {
                new UnitOfSizeModelDetail { Name = "Xen-ti-mét" },
                new UnitOfSizeModelDetail { Name = "Mét" }
            };
        }
    }

    public class UnitOfSizeModelDetail
    {
        public string Name { get; set; }
    }
}