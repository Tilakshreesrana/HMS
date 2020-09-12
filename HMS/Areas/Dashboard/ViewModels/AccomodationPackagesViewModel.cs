using HMS.Entities.Models;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationPackagesViewModel
    {
        public string searchTerm { get; set; }
        public int? accomodationTypeID { get; set; }
        public IEnumerable<AccomodationPackage> accomodationPackages { get; set; }
        public IEnumerable<AccomodationType> accomodationTypes { get; set; }
        public List<AccomodationPackageImage> acoomodationPackageImages { get; set; }

        public Pager pager { get; set; }
    }
}