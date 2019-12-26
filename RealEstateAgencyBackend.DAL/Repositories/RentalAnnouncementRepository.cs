using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public class RentalAnnouncementRepository : IRentalAnnouncementRepository
    {
        private AppDbContext _context;

        public RentalAnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        public RentalAnnouncement Create(RentalAnnouncement item)
        {
            _context.RentalAnnouncements.Add(item);
            return item;
        }

        public RentalAnnouncement Remove(int id)
        {
            RentalAnnouncement announcement = _context.RentalAnnouncements.Find(id);
            if (announcement != null)
            {
                if (announcement.Reservations != null)
                {
                    _context.Reservations.RemoveRange(announcement.Reservations);
                }
                announcement = _context.RentalAnnouncements.Remove(announcement);
            }
            return announcement;
        }

        public RentalAnnouncement Find(int id)
        {
            return _context.RentalAnnouncements.Find(id);
        }

        public IQueryable<RentalAnnouncement> GetAll()
        {
            // Database query is performend when 'All' is called.
            // It means that all filtering that you do in services is filtering in dotnet list that is already returned from database.
            // It leads to database perfomance issues because you get too much data that you don't need.
            // You should return IQueryable here.

            return _context.RentalAnnouncements;

            /*return _context.RentalAnnouncements
                .Where(announcement => announcement.Reservations
                    .All(reservation => (!reservation.IsActive && reservation.IsConfirmed)));*/

        }

        public IEnumerable<RentalAnnouncement> FindByUserId(string userID)
        {
            return _context.RentalAnnouncements.Where(announcement => announcement.UserId == userID);
        }

        public RentalAnnouncement Update(RentalAnnouncement item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }
    }
}
