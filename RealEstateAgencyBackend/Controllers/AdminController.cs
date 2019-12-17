
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
        [Authorize]
        public IQueryable<CreateModel> GetUsers()
        {
            var users= UserManager.Users;
            List<CreateModel> userModels = new List<CreateModel>();
            foreach (var user in users)
            {
                userModels.Add(new CreateModel() {Name = user.UserName, Email = user.Email, Password = user.PasswordHash, LastName = user
                .UserLastName});
            }
            return userModels.AsQueryable();
            
        }

        private AppUserManager UserManager
        {
            get  
            {
             //HttpContext.Current.GetOwinContext
                return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

            }
        }

        // POST: api/RentalAnnouncements
        [ResponseType(typeof(RentalAnnouncement))]
        public IHttpActionResult PostRegister(CreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User { UserName = model.Name, Email = model.Email};
            IdentityResult result = UserManager.Create(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                AddErrorsFromResult(result);
                return BadRequest(ModelState);
                //return Ok();
            }
        }


        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = UserManager.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            UserManager.Delete(user);

            return Ok(user);
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
