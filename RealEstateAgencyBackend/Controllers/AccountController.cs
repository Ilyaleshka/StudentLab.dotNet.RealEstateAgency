using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RealEstateAgencyBackend.BLL;
using RealEstateAgencyBackend.BLL.DTO;
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

        public AccountController(IUserService service)
        {
            userService = service;
        }


        [Route("api/account/login")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginViewModel model)
        {
            UserDto user = userService.Find(model.Name, model.Password);

            if (user == null)
            {
                String errorResponce = String.Empty;
                errorResponce = string.Join(";\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                errorResponce = string.Join(";\n", errorResponce, "Incorrect login or password");
                return BadRequest(errorResponce);
            }
            else
            {
                ClaimsIdentity ident = userService.CreateIdentity(model.Name, model.Password, DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
            }

            return Ok(new ResponceModel() {Id = user.Id, Name = user.UserName,LastName = user.UserLastName, Email=user.Email });
        }

        [Route("api/account/logout")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Logout()
        {
            if (AuthManager.User != null)            
                AuthManager.SignOut();
            return Ok(); 
        }


        [Route("api/account/register")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Register(CreateModel model)
        {
            if (!ModelState.IsValid)
            {
                String errorResponce = String.Empty;
                errorResponce = string.Join(";\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errorResponce);
            }

            //User user = new User { UserName = model.Name, Email = model.Email,UserLastName = model.LastName };
            CreateUserDto user = new CreateUserDto { UserName = model.Name, Email = model.Email, UserLastName = model.LastName, Password = model.Password };
            IdentityResult result = userService.Create(user);

            if (result.Succeeded)
            {
                return Created("api/account/register", model);
            }
            else
            {

                return BadRequest(result.Errors.FirstOrDefault());
            }
        }


        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}
