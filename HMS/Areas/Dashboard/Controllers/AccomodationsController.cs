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
    public class AccomodationsController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public AccomodationsController(IRepositoryWrapper repowrapper)
        {
            _repoWrapper = repowrapper;

        }
        // GET: Dashboard/Accomodations
        public ActionResult Index(string searchTerm, int? AccomodationPackageID, int? pageNo)
        {
            int recordSize = 3;
            pageNo = pageNo ?? 1;
            IEnumerable<Accomodation> accomodations = _repoWrapper.Accomodations.GetAll();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (AccomodationPackageID.HasValue && AccomodationPackageID > 0)
            {
                accomodations = accomodations.Where(x => x.AccomodationPackageID == AccomodationPackageID);
            }
            var skip = (pageNo.Value - 1) * recordSize;
            AccomodationsViewModel model = new AccomodationsViewModel();
            model.pager = new Pager(accomodations.Count(), pageNo, recordSize);
            accomodations = accomodations.Skip(skip).Take(recordSize);


            model.searchTerm = searchTerm;
            model.accomodationPackageID = AccomodationPackageID;
            model.accomodations = accomodations;
            model.accomodationPackages = _repoWrapper.AccomodationPackages.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationsActionViewModel model = new AccomodationsActionViewModel();
            if (ID.HasValue)
            {
                var accomodation = _repoWrapper.Accomodations.Get(ID.Value);
                model.ID = accomodation.ID;
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
                
            }
            model.AccomodationPackages = _repoWrapper.AccomodationPackages.GetAll().ToList();
            return PartialView("_action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationsActionViewModel model)
        {
            JsonResult json = new JsonResult();

            Accomodation result = null;
            if (model.ID > 0)
            {
                Accomodation accomodation = _repoWrapper.Accomodations.Get(model.ID);
                accomodation.Name = model.Name;
                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Description = model.Description;
               
                result = _repoWrapper.Accomodations.Update(accomodation);
                _repoWrapper.Accomodations.Save();

            }
            else
            {
                Accomodation accomodation = new Accomodation();
                accomodation.Name = model.Name;
                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Description = model.Description;
                result = _repoWrapper.Accomodations.Add(accomodation);
                _repoWrapper.Accomodations.Save();

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
            AccomodationsActionViewModel model = new AccomodationsActionViewModel();
            model.ID = ID;

            return PartialView("_Delete", model);
        }
        [HttpPost]
        public JsonResult Delete(AccomodationsActionViewModel model)
        {
            JsonResult json = new JsonResult();

            Accomodation accomodation = _repoWrapper.Accomodations.Get(model.ID);

            _repoWrapper.Accomodations.Remove(accomodation);
            _repoWrapper.Accomodations.Save();


            json.Data = new { Success = false, Message = "Accomodation packages deleted successfully" };

            _repoWrapper.Accomodations.Save();
            return json;
        }
    }
}