using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository
{
    public class AccomodationsRepository:RepositoryBase<Accomodation>,IAccomodationsRepository
    {
        public AccomodationsRepository(HMSContext context) : base(context)
        {

        }
    }
}
