using HMS.Data.Repository.Repositories;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public HomeController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;

        }
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.accomodationTypes = _repoWrapper.AccomodationTypes.GetAll().ToList();
            model.accomodationPackages = _repoWrapper.AccomodationPackages.GetAll().ToList();

            return View(model);
        }

    }
}