using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationPackageActionViewModel
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerNight { get; set; }
        public string Description { get; set; }
        public string ImageIDs { get; set; }
        public List<AccomodationType> AccomodationTypes { get; set; }
        public List<AccomodationPackageImage> acoomodationPackageImages { get; set; }

    }
}