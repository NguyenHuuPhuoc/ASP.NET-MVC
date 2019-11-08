using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace ASPNetIdentityManagment.IRepository
{
    public interface IUserGroupRepository : IRepositoryBase<UserGroup, int>
    {
        IList<UserGroup> GetUsersByGroupId(int groupId);
    }
}