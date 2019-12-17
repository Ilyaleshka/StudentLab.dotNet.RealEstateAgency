using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;
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

        public void Create(RentalAnnouncement item)
        {
            _context.RentalAnnouncements.Add(item);
        }

        public RentalAnnouncement Remove(int id)
        {
            RentalAnnouncement request = _context.RentalAnnouncements.Find(id);
            if (request != null)
                request = _context.RentalAnnouncements.Remove(request);
            return request;
        }

        public RentalAnnouncement Find(int id)
        {
            return _context.RentalAnnouncements.Find(id);
        }

        public IEnumerable<RentalAnnouncement> GetAll()
        {
            return _context.RentalAnnouncements;
        }

        public IEnumerable<RentalAnnouncement> GetByUserId(string userID)
        {
            return _context.RentalAnnouncements.Where(request => request.UserId == userID);
        }

        public void Update(RentalAnnouncement item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
