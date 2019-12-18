using RealEstateAgencyBackend.BLL.DTO;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalAnnouncementService
    {
        IEnumerable<RentalAnnouncementDto> GetAll();

        RentalAnnouncementDto Find(int id);

        RentalAnnouncementDto Remove(RentalAnnouncementDto rentalAnnouncement);

        void Update(RentalAnnouncementDto rentalAnnouncement);

        void Create(RentalAnnouncementDto rentalAnnouncement);

    }
}
