using HMS.Areas.Dashboard.ViewModels;
using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        // GET: Dashboard/AccomodationTypes

        public AccomodationPackagesController(IRepositoryWrapper repowrapper)
        {
            _repoWrapper = repowrapper;

        }
        // GET: Dashboard/AccomodationPackages
        public ActionResult Index(string searchTerm,int? AccomodationTypeID,int?pageNo)
        {
            int recordSize = 3;
            pageNo = pageNo ?? 1;
            IEnumerable<AccomodationPackage> accPackages = _repoWrapper.AccomodationPackages.GetAll();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accPackages = accPackages.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if(AccomodationTypeID.HasValue && AccomodationTypeID>0)
            {
                accPackages = accPackages.Where(x => x.AccomodationTypeID == AccomodationTypeID);
            }
            var skip = (pageNo.Value - 1) * recordSize;
            AccomodationPackagesViewModel model = new AccomodationPackagesViewModel();
            model.pager = new Pager(accPackages.Count(), pageNo, recordSize);
            accPackages = accPackages.Skip(skip).Take(recordSize);

            
            model.searchTerm = searchTerm;
            model.accomodationTypeID = AccomodationTypeID;
            model.accomodationPackages = accPackages;
            model.accomodationTypes = _repoWrapper.AccomodationTypes.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPackageActionViewModel model = new AccomodationPackageActionViewModel();
            if (ID.HasValue)
            {
                var accomodationPackage = _repoWrapper.AccomodationPackages.Get(ID.Value);
                model.ID = accomodationPackage.ID;
                model.AccomodationTypeID = accomodationPackage.AccomodationTypeID;
                model.Name = accomodationPackage.Name;
                model.NoOfRooms = accomodationPackage.NoOfRooms;
                model.FeePerNight = accomodationPackage.FeePerNight;
                model.Description = accomodationPackage.Description;
                model.acoomodationPackageImages = accomodationPackage.AccomodationPackageImages;
               
            }
            model.AccomodationTypes = _repoWrapper.AccomodationTypes.GetAll().ToList();
            return PartialView("_action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationPackageActionViewModel model)
        {
            JsonResult json = new JsonResult();

            AccomodationPackage result = null;
            List<int> imgIds = !string.IsNullOrEmpty(model.ImageIDs)? (model.ImageIDs).Split(',').Select(x => int.Parse(x)).ToList():new List<int>();
            //var images = imgIds.Select(x =>_repoWrapper.Images.Find(x)).ToList();
            var images = _repoWrapper.Images.GetAll().Where(x => imgIds.Contains(x.ID)).ToList();
            if (model.ID > 0)
            {
                AccomodationPackage accomodationPackage = _repoWrapper.AccomodationPackages.Get(model.ID);
                accomodationPackage.Name = model.Name;
                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.NoOfRooms = model.NoOfRooms;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.Description = model.Description;

                //accomodationPackage.AccomodationPackageImages.Clear();
                _repoWrapper.AccomodationPackageImages.RemoveRange(accomodationPackage.AccomodationPackageImages);
                
                accomodationPackage.AccomodationPackageImages.AddRange(images.Select(x => new AccomodationPackageImage() { ImageID = x.ID }));
                result = _repoWrapper.AccomodationPackages.Update(accomodationPackage);
                _repoWrapper.AccomodationTypes.Save();

            }
            else
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();
                accomodationPackage.Name = model.Name;
                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.NoOfRooms = model.NoOfRooms;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.Description = model.Description;

                accomodationPackage.AccomodationPackageImages = new List<AccomodationPackageImage>();
                accomodationPackage.AccomodationPackageImages.AddRange(images.Select(x=>new AccomodationPackageImage() {ImageID=x.ID}));
               result = _repoWrapper.AccomodationPackages.Add(accomodationPackage);
                _repoWrapper.AccomodationPackages.Save();

            }

            if (result != null)
            {
                json.Data = new { Success = true };

            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to save accomodation package" };

            }
            _repoWrapper.AccomodationPackages.Save();
            return json;
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationTypeActionViewModel model = new AccomodationTypeActionViewModel();
            model.ID = ID;

            return PartialView("_Delete", model);
        }
        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionViewModel model)
        {
            JsonResult json = new JsonResult();

            AccomodationPackage accomodationPackage = _repoWrapper.AccomodationPackages.Get(model.ID);

            _repoWrapper.AccomodationPackages.Remove(accomodationPackage);
            _repoWrapper.AccomodationPackages.Save();


            json.Data = new { Success = false, Message = "Accomodation packages deleted successfully" };

            _repoWrapper.AccomodationPackages.Save();
            return json;
        }
    }
}