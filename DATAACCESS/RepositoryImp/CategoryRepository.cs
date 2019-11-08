using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DATAACCESS.RepositoryImp
{
    public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Category> GetByfilter(string keyWord, int? parentId, int? typeId, int currentPage, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.Categories select a;

            if (typeId.HasValue)
            {
                query = query.Where(n => n.Type == typeId.Value);
            }

            if (parentId.HasValue)
            {
                query = query.Where(n => n.ParentId == parentId.Value);
            }

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