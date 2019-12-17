using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RealEstateAgencyBackend.BLL;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.BLL.Services;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace RealEstateAgencyBackend.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        IUserService userService;

        public AccountController()
        {
            userService = new UserService(new UnitOfWork());
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginViewModel model)
        {
            User user = userService.Find(model.Name, model.Password);
            //User user = UserManager.Find(model.Name, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректное имя или пароль.");
            }
            else
            {
                //ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                ClaimsIdentity ident = userService.CreateIdentity(user,DefaultAuthenticationTypes.ApplicationCookie);


                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
            }

            return Ok(ModelState);
        }

        
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        /*
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }*/
    }
}
