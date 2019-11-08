using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace DATAACCESS.IRepository
{
    public interface IUnitRepository : IRepositoryBase<Unit, int>
    {
        IList<Unit> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);
    }
}