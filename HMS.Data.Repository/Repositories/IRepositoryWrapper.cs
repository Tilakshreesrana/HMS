using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository.Repositories
{
    public interface IRepositoryWrapper
    {
        IAccomodationTypesRepository AccomodationTypes { get;}
        IAccomodationPackagesRepository AccomodationPackages { get; }
        IAccomodationPackageImagesRepository AccomodationPackageImages { get; }
        IAccomodationsRepository Accomodations { get; }
        IImagesRepository Images { get; }
    }
}
