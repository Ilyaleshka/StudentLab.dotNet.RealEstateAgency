using Microsoft.Owin.Security;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RealEstateAgencyBackend.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [Route("api/profile/announcements")]
        [HttpPost]
        public IHttpActionResult Announcements()
        {
            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);
            IEnumerable<RentalAnnouncementDto> rentalAnnouncements = null;
            if (userId != null)
                rentalAnnouncements = _userService.GetRentalAnnouncements(userId);
            return Ok(rentalAnnouncements);
        }

        [Route("api/profile/requests")]
        [HttpGet]
        public IHttpActionResult Requests()
        {

            return Ok();
        }

        [Route("api/profile/reservations")]
        [HttpGet]
        public IHttpActionResult Reservations()
        {

            return Ok();
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
    }
}
