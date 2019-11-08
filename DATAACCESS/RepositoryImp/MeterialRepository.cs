using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DATAACCESS.RepositoryImp
{
    public class MeterialRepository : RepositoryBase<Meterial, int>, IMeterialRepository
    {
        public MeterialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Meterial> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.Meterials select a;

            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(n => n.Name.Contains(keyWord));
            }

            query = query.OrderByDescending(n => n.Id);

            totalRow = query.Count();

            var result = query.Skip(currentPage * pageSize).Take(pageSize).ToList();

            return result;
        }
    }
}