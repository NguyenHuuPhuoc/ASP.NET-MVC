using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace DATAACCESS.IRepository
{
    public interface ICategoryRepository : IRepositoryBase<Category, int>
    {
        IList<Category> GetByfilter(string keyWord, int? parentId, int? typeId, int currentPage, int pageSize, out int totalRow);
    }
}