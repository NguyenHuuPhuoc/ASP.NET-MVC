using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DATAACCESS.RepositoryImp
{
    public class DesignRepository : RepositoryBase<Design, int>, IDesignRepository
    {
        public DesignRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Design> GetByFilter(string keyWord, int? categoryParentId, int currentPage, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.Designs select a;

            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(n => n.Name.Contains(keyWord));
            }

            if (categoryParentId.HasValue)
            {
                query = query.Where(n => n.ParentCategoryId == categoryParentId.Value);
            }

            query = query.OrderByDescending(n => n.Id);

            totalRow = query.Count();

            var lst = query.Skip(pageSize * currentPage).Take(pageSize).ToList();

            return lst;
        }
    }
}