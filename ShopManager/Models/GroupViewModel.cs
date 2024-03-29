﻿using DOMAIN.Models;
using MvcPaging;
using System.Collections.Generic;

namespace ShopManager.Models
{
    public class GroupViewModel
    {
        public string KeyWord { get; set; }

        public IPagedList<Group> Groups { get; set; }
    }

    public class GroupModelDetail
    {
        public Group Group { get; set; }

        public List<RolesModelDetail> Roles { get; set; }
    }
}