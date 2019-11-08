using ASPNetIdentityManagment.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetIdentityManagment.RepositoryImp
{
    public class UserGroupRepository : RepositoryBase<UserGroup, int>, IUserGroupRepository
    {
        public UserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<UserGroup> GetUsersByGroupId(int groupId)
        {
            var query = from a in DbContext.UserGroups select a;

            query = query.OrderBy(n => n.GroupId);

            return query.ToList();
        }
    }
}