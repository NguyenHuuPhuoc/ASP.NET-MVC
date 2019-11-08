using DOMAIN.Models;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace ASPNetIdentityManagment.IRepository
{
    public interface IRoleGroupRepository : IRepositoryBase<RoleGroup, int>
    {
        IList<RoleGroup> GetRolesByGroupId(int groupId);

        void DeleteRoleOfGroup(int groupId);
    }
}