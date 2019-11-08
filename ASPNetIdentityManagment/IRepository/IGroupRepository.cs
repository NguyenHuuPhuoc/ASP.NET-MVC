using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace ASPNetIdentityManagment.IRepository
{
    public interface IGroupRepository : IRepositoryBase<Group, int>
    {
        IList<Group> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);
    }
}