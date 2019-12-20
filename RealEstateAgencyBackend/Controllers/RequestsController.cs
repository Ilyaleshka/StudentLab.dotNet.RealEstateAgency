using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;

namespace RealEstateAgencyBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RequestsController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        IRentalRequestService userService;

        public RequestsController(IRentalRequestService service)
        {
            userService = service;
        }

        // POST: api/RentalRequests
        [Route("api/requests/create")]
        [HttpPost]
        public IHttpActionResult CreateRequest(RentalRequestCreateViewModel rentalRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                    
            return Created("",rentalRequest);
        }


        // GET: api/RentalRequests
        public IQueryable<RentalRequest> GetRentalRequests()
        {
            return db.RentalRequests;
        }

        // GET: api/RentalRequests/5
        [ResponseType(typeof(RentalRequest))]
        public IHttpActionResult GetRentalRequest(int id)
        {
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            return Ok(rentalRequest);
        }

        // PUT: api/RentalRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRentalRequest(int id, RentalRequest rentalRequest)
        {
            if (!ModelState.IsValid)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RentalRequests
        [ResponseType(typeof(RentalRequest))]
        public IHttpActionResult PostRentalRequest(RentalRequest rentalRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RentalRequests.Add(rentalRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rentalRequest.Id }, rentalRequest);
        }

        // DELETE: api/RentalRequests/5
        [ResponseType(typeof(RentalRequest))]
        public IHttpActionResult DeleteRentalRequest(int id)
        {
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            db.RentalRequests.Remove(rentalRequest);
            db.SaveChanges();

            return Ok(rentalRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalRequestExists(int id)
        {
            return db.RentalRequests.Count(e => e.Id == id) > 0;
        }
    }
}