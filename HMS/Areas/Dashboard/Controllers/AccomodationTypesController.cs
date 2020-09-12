using HMS.Areas.Dashboard.ViewModels;
using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        // GET: Dashboard/AccomodationTypes

        public AccomodationTypesController(IRepositoryWrapper repowrapper)
        {
            _repoWrapper = repowrapper;

        }
        public ActionResult Index(string searchTerm)
        {
            IEnumerable<AccomodationType> accTypes = null;
            if (!string.IsNullOrEmpty(searchTerm))
            {
               accTypes =_repoWrapper.AccomodationTypes.GetAll().Where(x=>x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            else
            {
                accTypes = _repoWrapper.AccomodationTypes.GetAll();
            }
            AccomodationTypesViewModel model = new AccomodationTypesViewModel();
            model.searchTerm = searchTerm;
            model.accomodationTypes = accTypes;
            return View(model);
        }
        //public ActionResult Listing()
        //{
           
        //    return PartialView("_listingAccomodationTypes", model);
        //}
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationTypeActionViewModel model = new AccomodationTypeActionViewModel();
            if (ID.HasValue)
            {
                var accomodationType = _repoWrapper.AccomodationTypes.Get(ID.Value);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }
            return PartialView("_action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationTypeActionViewModel model)
        {
            JsonResult json = new JsonResult();

            AccomodationType result = null;
            if (model.ID > 0)
            {
                AccomodationType accomodationType = _repoWrapper.AccomodationTypes.Get(model.ID);
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;
                result = _repoWrapper.AccomodationTypes.Update(accomodationType);
                _repoWrapper.AccomodationTypes.Save();

            }
            else
            {
                AccomodationType accomodationType = new AccomodationType();
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;
                result = _repoWrapper.AccomodationTypes.Add(accomodationType);
                _repoWrapper.AccomodationTypes.Save();

            }

            if (result != null)
            {
                json.Data = new { Success = true };

            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to save accomodation type" };

            }
            _repoWrapper.AccomodationTypes.Save();
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
        public JsonResult Delete(AccomodationTypeActionViewModel model)
        {
            JsonResult json = new JsonResult();

            AccomodationType accomodationType = _repoWrapper.AccomodationTypes.Get(model.ID);

            _repoWrapper.AccomodationTypes.Remove(accomodationType);
            _repoWrapper.AccomodationTypes.Save();


            json.Data = new { Success = false, Message = "Accomodation type deleted successfully" };

            _repoWrapper.AccomodationTypes.Save();
            return json;
        }
    }
}