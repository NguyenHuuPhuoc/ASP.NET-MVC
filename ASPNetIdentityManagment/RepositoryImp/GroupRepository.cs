﻿using System.Linq;
using System.Collections.Generic;
using ASPNetIdentityManagment.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;

namespace ASPNetIdentityManagment.RepositoryImp
{
    public class GroupRepository : RepositoryBase<Group, int>, IGroupRepository
    {
        public GroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Group> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.Groups select a;

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(n => n.Name.Contains(keyWord));
            }

            query = query.OrderByDescending(n => n.Id);

            totalRow = query.Count();

            var result = query.Skip(pageSize * currentPage).Take(pageSize).ToList();

            return result;
        }
    }
}