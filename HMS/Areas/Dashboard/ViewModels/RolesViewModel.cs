using HMS.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class RolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string searchTerm { get; set; }
        public Pager pager { get; set; }
    }
}