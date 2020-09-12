using HMS.Areas.Dashboard.ViewModels;
using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        // GET: Dashboard/Users
        private readonly IRepositoryWrapper _repoWrapper;
        public UsersController(IRepositoryWrapper repowrapper, ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
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
        public async Task<ActionResult> Index(string searchTerm, string RoleID, int? pageNo)
        {
            int recordSize = 3;
            pageNo = pageNo ?? 1;
            IEnumerable<HMSUser> users = UserManager.Users;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(x => x.FullName.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(RoleID) && RoleID!="0")
            {
                var role = await RoleManager.FindByIdAsync(RoleID);
                var userIDs = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIDs.Contains(x.Id));
                //users = users.Where(x => x.Roles.Select(y=>y.RoleId).Contains(RoleID));
            }
            var skip = (pageNo.Value - 1) * recordSize;
            UsersViewModel model = new UsersViewModel();
            model.pager = new Pager(users.Count(), pageNo, recordSize);
            model.Users = users.Skip(skip).Take(recordSize);
            model.Roles = RoleManager.Roles;


            model.searchTerm = searchTerm;
            model.RoleID = RoleID;
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UsersActionViewModel model = new UsersActionViewModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var user = await UserManager.FindByIdAsync(ID);
                model.ID = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;

                //model.Name = accomodation.Name;
                //model.Description = accomodation.Description;

            }
            //model.Roles = _repoWrapper.AccomodationPackages.GetAll().ToList();
            return PartialView("_action", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(UsersActionViewModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                //model.ID = user.Id;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                //Accomodation accomodation = _repoWrapper.Accomodations.Get(model.ID);
                //accomodation.Name = model.Name;
                //accomodation.RoleID = model.RoleID;
                //accomodation.Description = model.Description;

                //result = _repoWrapper.Accomodations.Update(accomodation);
                result = await UserManager.UpdateAsync(user);
               

            }
            else
            {
                var user = new HMSUser();

                //model.ID = user.Id;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.CreateAsync(user);


            }


            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }
        [HttpGet]
        public ActionResult Delete(string ID)
        {
            UsersActionViewModel model = new UsersActionViewModel();
            model.ID = ID;

            return PartialView("_Delete", model);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionViewModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                result = await UserManager.DeleteAsync(user);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid user" };
            }
            return json;
        }
        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
           
            UserRolesViewModel model = new UserRolesViewModel();
            var user = await UserManager.FindByIdAsync(ID);
           
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => userRoleIDs.Contains(x.Id)).ToList();
            model.Roles = RoleManager.Roles.Where(x => !userRoleIDs.Contains(x.Id));
            model.UserID = ID;

            return PartialView("_userRoles", model);
        }
        [HttpPost]
        public async Task<JsonResult> UserRoleAction(string UserId,string RoleId,bool isDelete=false )
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;
          
                var user = await UserManager.FindByIdAsync(UserId);
                var role = await RoleManager.FindByIdAsync(RoleId);

             
                //result = await UserManager.UpdateAsync(user);


            if(user!=null && role != null)
            {
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(UserId, role.Name);
                    json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
                }
                else 
                {
                    result = await UserManager.RemoveFromRoleAsync(UserId, role.Name);
                    json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

                }


            }
            else
            {
                json.Data = new { Success = false, Message = "invalid Operation" };

            }


            
            return json;
        }
    }
}