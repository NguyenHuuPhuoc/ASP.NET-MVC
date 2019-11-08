using ASPNetIdentityManagment.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ASPNetIdentityManagment.RepositoryImp
{
    public class RoleGroupRepository : RepositoryBase<RoleGroup, int>, IRoleGroupRepository
    {
        public RoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void DeleteRoleOfGroup(int groupId)
        {
            DeleteMulti(n => n.GroupId == groupId);
        }

        public IList<RoleGroup> GetRolesByGroupId(int groupId)
        {
            var query = from a in DbContext.RoleGroups where a.GroupId == groupId select a;

            query = query.OrderBy(n => n.RoleId);

            return query.ToList();
        }
    }
}