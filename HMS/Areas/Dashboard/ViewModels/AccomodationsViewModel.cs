using HMS.Entities.Models;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationsViewModel
    {
        public string searchTerm { get; set; }
        public int? accomodationPackageID { get; set; }
        public IEnumerable<Accomodation> accomodations { get; set; }
        public IEnumerable<AccomodationPackage> accomodationPackages { get; set; }

        public Pager pager { get; set; }
    }
}