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
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;

namespace RealEstateAgencyBackend.Controllers
{
    [EnableCors(  "*",  "*", "*")]
    public class AnnouncementsController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/RentalAnnouncements
        public IQueryable<RentalAnnouncement> GetRentalAnnouncements()
        {
            return db.RentalAnnouncements;
        }

        // GET: api/RentalAnnouncements/5
        [ResponseType(typeof(RentalAnnouncement))]
        public IHttpActionResult GetRentalAnnouncement(int id)
        {
            RentalAnnouncement rentalAnnouncement = db.RentalAnnouncements.Find(id);
            if (rentalAnnouncement == null)
            {
                return NotFound();
            }

            return Ok(rentalAnnouncement);
        }

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

        // POST: api/RentalAnnouncements
        [ResponseType(typeof(RentalAnnouncement))]
        public IHttpActionResult PostRentalAnnouncement(RentalAnnouncement rentalAnnouncement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RentalAnnouncements.Add(rentalAnnouncement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rentalAnnouncement.Id }, rentalAnnouncement);
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalAnnouncementExists(int id)
        {
            return db.RentalAnnouncements.Count(e => e.Id == id) > 0;
        }
    }
}