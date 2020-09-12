using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationTypesViewModel
    {
        public string searchTerm { get; set; }
        public IEnumerable<AccomodationType> accomodationTypes { get; set; }
    }
}