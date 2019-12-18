using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.DAL.Repositories;

namespace RealEstateAgencyBackend.DAL.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext dbContext;

        private RentalRequestRepository _rentalRequestRepository;
        private RentalAnnouncementRepository _rentalAnnouncementRepository;
        private UserRepository _userRepository;

        public UnitOfWork(AppDbContext context)
        {
            dbContext = context;
        }

        public IRentalRequestRepository RentalRequestRepository
        {
            get
            {
                if (_rentalRequestRepository == null)
                    _rentalRequestRepository = new RentalRequestRepository(dbContext);
                return _rentalRequestRepository;
            }
        }
        public IRentalAnnouncementRepository RentalAnnouncementRepository
        {
            get
            {
                if (_rentalAnnouncementRepository == null)
                    _rentalAnnouncementRepository = new RentalAnnouncementRepository(dbContext);
                return _rentalAnnouncementRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(dbContext);//
                return _userRepository;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
