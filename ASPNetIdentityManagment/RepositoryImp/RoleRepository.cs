using ASPNetIdentityManagment.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetIdentityManagment.RepositoryImp
{
    public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Role> GetByFilter(string roleName, int currentPage, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.ApplicationRoles select a;

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                query = query.Where(n => n.Name.Contains(roleName));
            }

            query = query.OrderByDescending(n => n.Id);

            totalRow = query.Count();

            var result = query.Skip(pageSize * currentPage).Take(pageSize).ToList();

            return result;
        }
    }
}