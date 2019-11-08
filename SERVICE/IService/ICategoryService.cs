using DOMAIN.Models;
using System.Collections.Generic;

namespace SERVICE.IService
{
    public interface ICategoryService : IBaseService
    {
        void CreateNew(Category category);

        void Update(Category category);

        void Delete(int id);

        Category GetByKey(int id);

        bool CheckContain(string code);

        bool CheckContain(int id, string code);

        IList<Category> GetByfilter(string keyWord, int? parentId, int? typeId, int currentPage, int pageSize, out int totalRow);

        IEnumerable<Category> GetParents();

        IEnumerable<Category> GetParents(int typeId);
    }
}