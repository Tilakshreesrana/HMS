using HMS.Entities.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class UsersViewModel
    {
        public string searchTerm { get; set; }
        public string RoleID { get; set; }
        public IEnumerable<HMSUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public Pager pager { get; set; }
    }
}