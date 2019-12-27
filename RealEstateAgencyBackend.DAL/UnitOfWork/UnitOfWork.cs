using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.DAL.Repositories;

namespace RealEstateAgencyBackend.DAL.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _dbContext;

        private RentalRequestRepository _rentalRequestRepository;
        private RentalAnnouncementRepository _rentalAnnouncementRepository;
        private UserRepository _userRepository;
        private ReservationRepository _reservationRepository;
        private ImageRepository _imageRepository;

        public UnitOfWork(AppDbContext context)
        {
            _dbContext = context;
        }

        public IRentalRequestRepository RentalRequestRepository
        {
            get
            {
                if (_rentalRequestRepository == null)
                    _rentalRequestRepository = new RentalRequestRepository(_dbContext);
                return _rentalRequestRepository;
            }
        }
        public IRentalAnnouncementRepository RentalAnnouncementRepository
        {
            get
            {
                if (_rentalAnnouncementRepository == null)
                    _rentalAnnouncementRepository = new RentalAnnouncementRepository(_dbContext);
                return _rentalAnnouncementRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);//
                return _userRepository;
            }
        }

        public IReservationRepository ReservationRepository
        {
            get
            {
                if (_reservationRepository == null)
                    _reservationRepository = new ReservationRepository(_dbContext);//
                return _reservationRepository;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new ImageRepository(_dbContext);//
                return _imageRepository ;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
