
using RealEstateAgencyBackend.Models;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using RealEstateAgencyBackend.BLL;

namespace RealEstateAgencyBackend.Controllers
{

    public class AdminController : System.Web.Http.ApiController
    {
        //[Authorize]

        //It's only for debug, dont pay attention
        public IQueryable<UserCreateModel> GetUsers()
        {
            var users= UserManager.Users;
            List<UserCreateModel> userModels = new List<UserCreateModel>();
            foreach (var user in users)
            {
                userModels.Add(new UserCreateModel() {Name = user.UserName, Email = user.Email, Password = user.PasswordHash, LastName = user
                .UserLastName});
            }
            return userModels.AsQueryable();
            
        }

        private AppUserManager UserManager
        {
            get  
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        public IHttpActionResult DeleteUser(string id)
        {/*
            User user = UserManager.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            UserManager.Delete(user);
            */
            return Ok();
        }

        private void AddErrorsFromResult(IdentityResult result)
        {/*
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }*/
        }

    }
}
