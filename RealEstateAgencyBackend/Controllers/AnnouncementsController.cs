using AutoMapper;
using Microsoft.Owin.Security;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace RealEstateAgencyBackend.Controllers
{
    public class AnnouncementsController : ApiController
    {
        private IMapper _mapper;
        private IRentalAnnouncementService _rentalAnnouncementService;
        private IUserService _userService;
        private IImageService _imageService;

        public AnnouncementsController(IRentalAnnouncementService rentalAnnouncementService, IUserService userService,IImageService imageService, IMapper mapper)
        {
            _rentalAnnouncementService = rentalAnnouncementService;
            _userService = userService;
            _mapper = mapper;
            _imageService = imageService;
        }


        [HttpGet]
        [Route("api/announcements")]
        public RentalAnnouncementPageViewModel GetRentalAnnouncements(int page,int pageSize)
        {
			RentalAnnouncementPageDto announcements = _rentalAnnouncementService.GetPageWithFilters(page, pageSize, HttpContext.Current.Request.QueryString);
            RentalAnnouncementPageViewModel results = _mapper.Map<RentalAnnouncementPageViewModel>(announcements);
            return results;
        }


		[HttpGet]
        [Route("api/announcements/{id:int}")]
        [ResponseType(typeof(RentalAnnouncementReservationViewModel))]
        public IHttpActionResult GetRentalAnnouncement(int id)
        {
            RentalAnnouncementReservationDto rentalAnnouncement = _rentalAnnouncementService.GetFullInfo(id);

            if (rentalAnnouncement == null)
                return NotFound();

            RentalAnnouncementReservationViewModel view = _mapper.Map<RentalAnnouncementReservationViewModel>(rentalAnnouncement);

            return Ok(view);
        }


        [Authorize]
        [HttpPost]
        [Route("api/announcements/create")]
        [ResponseType(typeof(RentalAnnouncementViewModel))]
        public IHttpActionResult Create(RentalAnnouncementCreateModel rentalAnnouncementCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //var folderPath = HttpContext.Current.Server.MapPath("~/Public/");
			var folderPath = "Public";
			var appFolderPath = HttpContext.Current.Server.MapPath("~/");

			ICollection<ImageDto> images = new List<ImageDto>();
            foreach (var img in rentalAnnouncementCreateModel.Base64Images)
            {
                images.Add(_imageService.Save(img, appFolderPath ,folderPath));
            }

            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);

            RentalAnnouncementDto rentalAnnouncement = _mapper.Map<RentalAnnouncementDto>(rentalAnnouncementCreateModel);//RentalAnnouncementCreateModel
            rentalAnnouncement.UserId = userId;
            rentalAnnouncement.Images = images;

			RentalAnnouncementDto createdRentalAnnouncement = _rentalAnnouncementService.Create(rentalAnnouncement);

            return Created("", _mapper.Map<RentalAnnouncementViewModel>(createdRentalAnnouncement));
        }

        // DELETE: api/RentalAnnouncements/5
        [Authorize]
        [HttpDelete]
        [Route("api/announcements/{id}")]
        [ResponseType(typeof(RentalAnnouncementViewModel))]
        public IHttpActionResult DeleteRentalAnnouncement(int id)
        {
            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);

            RentalAnnouncementDto announcement = _rentalAnnouncementService.Find(id);

            if (announcement == null || announcement.UserId != userId)
                return NotFound();

            RentalAnnouncementDto deletedAnnouncement = _rentalAnnouncementService.Remove(announcement);

            return Ok(_mapper.Map<RentalAnnouncementViewModel>(deletedAnnouncement));
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