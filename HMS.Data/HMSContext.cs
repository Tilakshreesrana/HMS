using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HMS.Entities.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Data
{
    public class HMSContext :IdentityDbContext<HMSUser>
    {
        public HMSContext() : base("HMSConnectionString")
        {
        }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<AccomodationPackageImage> AccomodationPackageImages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Image> Images { get; set; }
        public static HMSContext Create()
        {
            return new HMSContext();

        }
    }
}
