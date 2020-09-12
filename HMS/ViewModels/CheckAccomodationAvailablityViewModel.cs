using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class CheckAccomodationAvailablityViewModel
    {
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
        public string Name { get; set; }
        public string NoOfEmailAdults { get; set; }
        public string Notes { get; set; }
    }
}