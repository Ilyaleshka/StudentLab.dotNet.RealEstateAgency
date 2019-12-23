using AutoMapper;
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
        IUserService _userService;
        private IMapper _mapper;

        public AccountController(IUserService service,IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }


        [Route("api/account/login")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }

            UserDto user = _userService.Find(model.Name, model.Password);

            if (user == null)
            {
                return BadRequest("Incorrect login or password");
            }
            else
            {
                ClaimsIdentity ident = _userService.CreateIdentity(model.Name, model.Password, DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                },
                ident);
            }

            //return Ok(new UserViewModel() {Id = user.Id, Name = user.UserName,LastName = user.UserLastName, Email=user.Email });
            return Ok(_mapper.Map<UserViewModel>(user));
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
        public IHttpActionResult Register(UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                String errorResponce = String.Empty;
                errorResponce = string.Join(";\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errorResponce);
            }

            CreateUserDto user =_mapper.Map<CreateUserDto>(model);
            IdentityResult result = _userService.Create(user);

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

        [NonAction]
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}
