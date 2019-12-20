using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private AppDbContext _context;

        public RentalRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(RentalRequest item)
        {
            _context.RentalRequests.Add(item);
        }

        public RentalRequest Remove(int id)
        {
            RentalRequest request = _context.RentalRequests.Find(id);
            if (request != null)
                request = _context.RentalRequests.Remove(request);
            return request;
        }

        public RentalRequest Find(int id)
        {
            return _context.RentalRequests.Find(id);
        }

        public IEnumerable<RentalRequest> GetAll()
        {
            return _context.RentalRequests;
        }

        public IEnumerable<RentalRequest> FindByUserId(string userID)
        {
            return _context.RentalRequests.Where(request => request.UserId == userID);
        }

        public void Update(RentalRequest item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
