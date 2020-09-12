using HMS.Data.Repository.Repositories;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccomodationsController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public AccomodationsController(IRepositoryWrapper repowrapper)
        {
            _repoWrapper = repowrapper;

        }
        // GET: Accomodations
        public ActionResult Index(int accomodationTypeID,int? accomodationPackageID)
        {
            AccomodationsFEViewModel model = new AccomodationsFEViewModel();
            model.accomodationType = _repoWrapper.AccomodationTypes.Get(accomodationTypeID);
            model.accomodationPackages = _repoWrapper.AccomodationPackages.GetAll().Where(x => x.AccomodationTypeID == accomodationTypeID).ToList();
            int firstAccomodationPkgID = model.accomodationPackages.First().ID;
            model.selectedAccomodationPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : firstAccomodationPkgID;
            //UploadPictures();
            model.accomodations = _repoWrapper.Accomodations.GetAll().Where(x => x.AccomodationPackageID == model.selectedAccomodationPackageID).ToList();
            return View(model);
            
        }
        public ActionResult Details(int accomodationPackageID)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();
            model.accomodationPackage  = _repoWrapper.AccomodationPackages.Get(accomodationPackageID);
           
            return View(model);

        }

        public ActionResult CheckAvailablity(CheckAccomodationAvailablityViewModel model)
        {
            
            return View();

        }



    }
}