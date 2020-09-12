using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class UsersActionViewModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        //public string RoleID { get; set; }
        //public IdentityRole Role { get; set; }
        //public List<IdentityRole> Roles { get; set; }
    }
}