using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class AccomodationsFEViewModel
    {
        public AccomodationType accomodationType { get; set; }
        public List<AccomodationPackage> accomodationPackages { get; set; }
        public List<Accomodation> accomodations { get; set; }
        public int selectedAccomodationPackageID { get; set; }
    }
}