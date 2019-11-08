using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace ASPNetIdentityManagment.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Role, int>
    {
        IList<Role> GetByFilter(string roleName, int currentPage, int pageSize, out int totalRow);
    }
}