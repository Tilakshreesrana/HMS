using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationsActionViewModel
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public  AccomodationPackage AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AccomodationPackage> AccomodationPackages { get; set; }
    }
}