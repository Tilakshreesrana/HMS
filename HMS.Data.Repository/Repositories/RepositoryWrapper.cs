using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IAccomodationTypesRepository _accomodationTypes;
        private IAccomodationPackagesRepository _accomodationPackages;
        private IAccomodationsRepository _accomodations;
        private IImagesRepository _images;
        private IAccomodationPackageImagesRepository _accomodationPackageImages;
        private HMSContext _hmsContext;
        public RepositoryWrapper(HMSContext hmsContext)
        {
            _hmsContext = hmsContext;

        }
        public IAccomodationTypesRepository AccomodationTypes
        {
            //this is the place where you need to add a new implementation each time
            get
            {
                if (_accomodationTypes == null)
                {
                    _accomodationTypes = new AccomodationTypesRepository(_hmsContext);
                }

                return _accomodationTypes;
            }
        }

        public IAccomodationPackagesRepository AccomodationPackages
        {
            get
            {
                if (_accomodationPackages == null)
                {
                    _accomodationPackages = new AccomodationPackagesRepository(_hmsContext);
                }

                return _accomodationPackages;
            }

        }
        public IAccomodationsRepository Accomodations
        {
            get
            {
                if (_accomodations == null)
                {
                    _accomodations = new AccomodationsRepository(_hmsContext);
                }

                return _accomodations;
            }

        }
        public IImagesRepository Images
        {
            get
            {
                if (_images == null)
                {
                    _images = new ImagesRepository(_hmsContext);
                }

                return _images;
            }

        }
        public IAccomodationPackageImagesRepository AccomodationPackageImages
        {
            get
            {
                if (_accomodationPackageImages == null)
                {
                    _accomodationPackageImages = new AccomodationPackageImageRepository(_hmsContext);
                }

                return _accomodationPackageImages;
            }

        }
    }
}
