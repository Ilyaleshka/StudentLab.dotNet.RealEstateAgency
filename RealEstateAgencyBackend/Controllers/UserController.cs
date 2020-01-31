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
            IEnumerable<RentalAnnouncementReservationDto> rentalAnnouncements = null;
            if (userId != null)
                rentalAnnouncements = _userService.GetRentalAnnouncements(userId);
            return Ok(rentalAnnouncements);
        }

        [Route("api/profile/requests")]
        [HttpPost]
        public IHttpActionResult Requests()
        {
            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);
            IEnumerable<RentalRequestDto> rentalRequests = null;
            if (userId != null)
                rentalRequests = _userService.GetRentalRequests(userId);
            return Ok(rentalRequests);
        }


        [Route("api/profile/reservations")]
        [HttpPost]
        public IHttpActionResult Reservations()
        {
			String userName = AuthManager.User.Identity.Name;
			String userId = _userService.GetUserId(userName);
			IEnumerable<RentalAnnouncementReservationDto> rentalAnnouncements = null;
			if (userId != null)
				rentalAnnouncements = _userService.GetReservations(userId);
			return Ok(rentalAnnouncements);
		}

		[HttpPost]
		[Route("api/profile/reservations/{announcementId:int}/reserve")]
		public IHttpActionResult ReserveAnnouncement(Int32 announcementId)
		{
			String userName = AuthManager.User.Identity.Name;
			String userId = _userService.GetUserId(userName);
			if (userId != null)
				_userService.ReserveAnnouncement(announcementId, userId);
			return Ok();
		}


		[HttpPost]
		[Route("api/profile/reservations/{announcementId:int}/accept")]
		public IHttpActionResult AcceptReservation(Int32 announcementId)
		{
			String userName = AuthManager.User.Identity.Name;
			String userId = _userService.GetUserId(userName);
			if (userId != null)
				_userService.ConfirmReservation(announcementId,userId);
			return Ok();
		}


		[HttpPost]
		[Route("api/profile/reservations/{announcementId:int}/reject")]
		public IHttpActionResult RejectReservations(Int32 announcementId)
		{
			String userName = AuthManager.User.Identity.Name;
			String userId = _userService.GetUserId(userName);
			if (userId != null)
				_userService.RejectReservation(announcementId, userId);
			return Ok();
		}


		[HttpPost]
		[Route("api/profile/reservations/{announcementId:int}/complete")]
		public IHttpActionResult CompliteReservations(Int32 announcementId)
		{
			String userName = AuthManager.User.Identity.Name;
			String userId = _userService.GetUserId(userName);
			if (userId != null)
				_userService.CompliteReservation(announcementId, userId);
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
