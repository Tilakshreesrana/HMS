﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Entities.Models
{
    public class AccomodationPackage
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerNight { get; set; }
        public string Description { get; set; }
        public virtual List<AccomodationPackageImage> AccomodationPackageImages { get; set; }
    }
}
