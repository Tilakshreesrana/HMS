using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class HomeViewModel
    {
        public List<AccomodationType> accomodationTypes { get; set; }
        public List<AccomodationPackage> accomodationPackages { get; set; }
    }
}