using RealEstateAgencyBackend.DAL.Repositories;

namespace RealEstateAgencyBackend.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRentalRequestRepository RentalRequestRepository { get; }

        IRentalAnnouncementRepository RentalAnnouncementRepository { get; }

        IUserRepository UserRepository { get; }

        void Save();
    }
}
