using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace DATAACCESS.IRepository
{
    public interface IDesignRepository : IRepositoryBase<Design, int>
    {
        IList<Design> GetByFilter(string keyWord, int? categoryParentId, int currentPage, int pageSize, out int totalRow);
    }
}