using HMS.Areas.Dashboard.ViewModels;
using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        // GET: Dashboard/Roles
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        // GET: Dashboard/Users
        private readonly IRepositoryWrapper _repoWrapper;
        public RolesController(IRepositoryWrapper repowrapper, ApplicationUserManager userManager, ApplicationSignInManager signInManager,ApplicationRoleManager roleManager)
        {
            _repoWrapper = repowrapper;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;

        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        } 
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
      

        // GET: Dashboard/Accomodations
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            int recordSize = 3;
            pageNo = pageNo ?? 1;
            IEnumerable<IdentityRole> roles = RoleManager.Roles;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.OrderBy(x=>x.Name).Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            
            var skip = (pageNo.Value - 1) * recordSize;
            RolesViewModel model = new RolesViewModel();
            model.pager = new Pager(roles.Count(), pageNo, recordSize);
            model.Roles = roles.Skip(skip).Take(recordSize);
            model.searchTerm = searchTerm;
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RolesActionViewModel model = new RolesActionViewModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var role = await RoleManager.FindByIdAsync(ID);
                model.ID = role.Id;
                model.Name = role.Name;

            }
            //model.Roles = _repoWrapper.AccomodationPackages.GetAll().ToList();
            return PartialView("_action", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(RolesActionViewModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);
                role.Name = model.Name;
                result = await RoleManager.UpdateAsync(role);
            } 
            else
            { 
                var role = new IdentityRole();
                role.Name = model.Name;
                result = await RoleManager.CreateAsync(role);

            }
            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }
        [HttpGet]
        public ActionResult Delete(string ID)
        {
            RolesActionViewModel model = new RolesActionViewModel();
            model.ID = ID;

            return PartialView("_Delete", model);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionViewModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);
                result = await RoleManager.DeleteAsync(role);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid user" };
            }
            return json;
        }
    }
}