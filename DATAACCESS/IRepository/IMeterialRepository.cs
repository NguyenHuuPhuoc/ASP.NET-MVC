using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace DATAACCESS.IRepository
{
    public interface IMeterialRepository : IRepositoryBase<Meterial, int>
    {
        IList<Meterial> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);
    }
}