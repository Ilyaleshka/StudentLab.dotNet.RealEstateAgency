using RealEstateAgencyBackend.Models;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalAnnouncementService
    {
        IEnumerable<RentalAnnouncement> GetAll();

        RentalAnnouncement Find(int id);

        RentalAnnouncement Remove(RentalAnnouncement rentalAnnouncement);

        void Update(RentalAnnouncement rentalAnnouncement);

        void Create(RentalAnnouncement rentalAnnouncement);

    }
}
