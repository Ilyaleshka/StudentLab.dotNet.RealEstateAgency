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
    public class RequestsController : ApiController
    {
        private IRentalRequestService _rentalRequestService;
        private IUserService _userService;
        private IMapper _mapper;

        public RequestsController(IRentalRequestService service, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _rentalRequestService = service;
            _mapper = mapper;
        }

        // POST: api/RentalRequests
        [Authorize]
        [Route("api/requests/create")]
        [HttpPost]
        public IHttpActionResult Create(RentalRequestCreateViewModel rentalRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);

            RentalRequestDto rentalRequest = _mapper.Map<RentalRequestDto>(rentalRequestCreateModel);
            rentalRequest.UserId = userId;
            RentalRequestDto createdRentalRequest = _rentalRequestService.Create(rentalRequest);

            return Ok(_mapper.Map<RentalRequestViewModel>(createdRentalRequest));
        }

        [HttpGet]
        [Route("api/requests")]
        public RentalRequestPageView GetRentalRequests(Int32 page,Int32 pageSize)
        {
			RentalRequestPageDto announcements = _rentalRequestService.GetPageWithFilters(page, pageSize, HttpContext.Current.Request.QueryString);
			RentalRequestPageView results = _mapper.Map<RentalRequestPageView>(announcements);
			return results;
			//var announcements = _rentalRequestService.GetAll();
			//IEnumerable<RentalRequestViewModel> results = _mapper.Map<IEnumerable<RentalRequestDto>, IEnumerable<RentalRequestViewModel>>(announcements);
			//return results;
		}

        // GET: api/RentalAnnouncements/5
        [HttpGet]
        [Route("api/requests/{id}")]
        [ResponseType(typeof(RentalRequestViewModel))]
        public IHttpActionResult GetRentalRequest(int id)
        {
            RentalRequestDto rentalRequest = _rentalRequestService.Find(id);

            if (rentalRequest == null)
                return NotFound();

            RentalRequestViewModel view = _mapper.Map<RentalRequestViewModel>(rentalRequest);

            return Ok(view);
        }



        // PUT: api/RentalRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRentalRequest(int id, RentalRequest rentalRequest)
        {
           /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rentalRequest.Id)
            {
                return BadRequest();
            }

            db.Entry(rentalRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [ResponseType(typeof(RentalRequestViewModel))]
        [Authorize]
        [Route("api/requests/{id}")]
        public IHttpActionResult DeleteRentalRequest(int id)
        {
            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);

            RentalRequestDto request = _rentalRequestService.Find(id);

            if (request.UserId != userId) 
                return NotFound();

            RentalRequestDto deletedRequest = _rentalRequestService.Remove(request);

            return Ok(_mapper.Map<RentalRequestViewModel>(deletedRequest));
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