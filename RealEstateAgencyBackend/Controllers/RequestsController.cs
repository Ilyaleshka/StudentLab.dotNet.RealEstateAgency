﻿using System;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        // You can use default 'Create' action and it will automatically map to correct url.
        public IHttpActionResult CreateRentalRequest(RentalRequestCreateViewModel rentalRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            String userName = AuthManager.User.Identity.Name;
            String userId = _userService.GetUserId(userName);

            RentalRequestDto rentalRequest = _mapper.Map<RentalRequestDto>(rentalRequestCreateModel);
            rentalRequest.UserId = userId;
            // Service should return new DTO and you should convert it to ViewModel and return.
            _rentalRequestService.Create(rentalRequest);

            // You can directly return ViewModel
            return Created("", rentalRequestCreateModel);
        }


        [Route("api/requests")]
        public IEnumerable<RentalRequestViewModel> GetRentalRequests()
        {
            var announcements = _rentalRequestService.GetAll();
            IEnumerable<RentalRequestViewModel> results = _mapper.Map<IEnumerable<RentalRequestDto>, IEnumerable<RentalRequestViewModel>>(announcements);
            return results;
        }

        // GET: api/RentalAnnouncements/5
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

        // POST: api/RentalRequests
        [ResponseType(typeof(RentalRequest))]
        public IHttpActionResult PostRentalRequest(RentalRequest rentalRequest)
        {/*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RentalRequests.Add(rentalRequest);
            db.SaveChanges();
            */
            return CreatedAtRoute("DefaultApi", new { id = rentalRequest.Id }, rentalRequest);
        }

        // DELETE: api/RentalRequests/5
        //[ResponseType(typeof(RentalRequest))]
        public IHttpActionResult DeleteRentalRequest(int id)
        {/*
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            db.RentalRequests.Remove(rentalRequest);
            db.SaveChanges();
            */
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