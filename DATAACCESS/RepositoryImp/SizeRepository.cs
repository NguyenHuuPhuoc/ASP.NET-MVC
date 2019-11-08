using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DATAACCESS.RepositoryImp
{
    public class SizeRepository : RepositoryBase<Size, int>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Size> GetByFilter(double? width, double? height, string unit, int currentPage, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.Sizes select a;

            if (width.HasValue)
            {
                query = query.Where(n => n.Width == width.Value);
            }

            if (height.HasValue)
            {
                query = query.Where(n => n.Height == height.Value);
            }

            if (!string.IsNullOrEmpty(unit))
            {
                query = query.Where(n => n.Unit.Contains(unit));
            }

            query = query.OrderByDescending(n => n.Id);

            totalRow = query.Count();

            var result = query.Skip(currentPage * pageSize).Take(pageSize).ToList();

            return result;
        }
    }
}