using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace DATAACCESS.IRepository
{
    public interface ISizeRepository : IRepositoryBase<Size, int>
    {
        IList<Size> GetByFilter(double? width, double? height, string unit, int currentPage, int pageSize, out int totalRow);
    }
}