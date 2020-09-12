using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository
{
    public class AccomodationPackagesRepository:RepositoryBase<AccomodationPackage>,IAccomodationPackagesRepository
    {
        public AccomodationPackagesRepository(HMSContext context) : base(context)
        {

        }

    }
}
