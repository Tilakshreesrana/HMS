using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository
{
    public class AccomodationTypesRepository :RepositoryBase<AccomodationType>,IAccomodationTypesRepository
    {
        public AccomodationTypesRepository(HMSContext context) : base(context)
        {

        }

    }
}
