using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Entities.Models
{
    public class Accomodation
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
