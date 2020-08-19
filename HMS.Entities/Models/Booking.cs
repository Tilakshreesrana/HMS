using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Entities.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public int AccomodationID { get; set; }
        public Accomodation accomodation { get; set; }
        public DateTime FromDate { get; set; }
        /// <summary>
        /// No Of Stay Nights
        /// </summary>
        public int Duration { get; set; }
    }
}
