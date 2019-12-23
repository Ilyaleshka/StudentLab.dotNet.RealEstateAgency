using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.Owin.Security;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;

namespace RealEstateAgencyBackend.Controllers
{
    [EnableCors(  "*",  "*", "*")]
    public class AnnouncementsController : ApiController
    {
        private IMapper _mapper;
        private IRentalAnnouncementService _rentalAnnouncementService;
        private IUserService _userService;

        public AnnouncementsController(IRentalAnnouncementService rentalAnnouncementService, IUserService userService, IMapper mapper)
        {
            _rentalAnnouncementService = rentalAnnouncementService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/announcements")]
        public IEnumerable<RentalAnnouncementViewModel> GetRentalAnnouncements()
        {
            var announcements = _rentalAnnouncementService.GetAll();
            IEnumerable<RentalAnnouncementViewModel> results = _mapper.Map<IEnumerable<RentalAnnouncementDto>, IEnumerable<RentalAnnouncementViewModel>>(announcements);
            return results;
        }

        [HttpGet]
        [Route("api/announcements/{id}")]
        [ResponseType(typeof(RentalAnnouncementViewModel))]
        public IHttpActionResult GetRentalAnnouncement(int id)
        {
            RentalAnnouncementDto rentalAnnouncement = _rentalAnnouncementService.Find(id);

            if (rentalAnnouncement == null)
                return NotFound();

            RentalAnnouncementViewModel view = _mapper.Map<RentalAnnouncementViewModel>(rentalAnnouncement);

            return Ok(view);
        }

        [Authorize]
        [Route("api/announcements/create")]
        [HttpPost]
        [ResponseType(typeof(RentalAnnouncementCreateModel))]
        public IHttpActionResult CreateRentalAnnouncement(RentalAnnouncementCreateModel rentalAnnouncementCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);

            RentalAnnouncementDto rentalAnnouncement = _mapper.Map<RentalAnnouncementDto>(rentalAnnouncementCreateModel);//RentalAnnouncementCreateModel
            rentalAnnouncement.UserId = userId;
            _rentalAnnouncementService.Create(rentalAnnouncement);

            return Created("", rentalAnnouncementCreateModel);
        }

        /*
        // PUT: api/RentalAnnouncements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRentalAnnouncement(int id, RentalAnnouncement rentalAnnouncement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rentalAnnouncement.Id)
            {
                return BadRequest();
            }

            db.Entry(rentalAnnouncement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalAnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        */

            /*
        // DELETE: api/RentalAnnouncements/5
        [ResponseType(typeof(RentalAnnouncement))]
        public IHttpActionResult DeleteRentalAnnouncement(int id)
        {
            RentalAnnouncement rentalAnnouncement = db.RentalAnnouncements.Find(id);
            if (rentalAnnouncement == null)
            {
                return NotFound();
            }

            db.RentalAnnouncements.Remove(rentalAnnouncement);
            db.SaveChanges();

            return Ok(rentalAnnouncement);
        }
        */

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
    }
}