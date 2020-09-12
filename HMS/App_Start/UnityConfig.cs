using HMS.Areas.Dashboard.Controllers;
using HMS.Data;
using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace HMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            var injectionConstructor = new InjectionConstructor(new HMSContext());

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IRepositoryWrapper, RepositoryWrapper>();
            //Did lot research on how to injet ApplicationUserManager and
            //AplicationSignInManager 
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationRoleManager>();
            //container.RegisterType<RolesController>(new InjectionConstructor());
            //container.RegisterType<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>(
            //    new InjectionConstructor(typeof(HMSContext)));
            //container.RegisterType<ApplicationRoleManager>(
            //   new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            //whereas thie below two methods were added for injecting ApplicationSignInManager and
            //ApplicationUserManager
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUserStore<HMSUser>, UserStore<HMSUser>>(
                new InjectionConstructor(typeof(HMSContext)));


            //this below method works for me for rolemanager
            container.RegisterType<IRoleStore<IdentityRole, string>,
              RoleStore<IdentityRole, string, IdentityUserRole>>(injectionConstructor);


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}