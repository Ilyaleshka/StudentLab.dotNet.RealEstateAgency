using RealEstateAgencyBackend.BLL.DTO;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalAnnouncementService
    {
        IEnumerable<RentalAnnouncementDto> GetAll();

        RentalAnnouncementDto Find(int id);

        RentalAnnouncementDto Remove(RentalAnnouncementDto rentalAnnouncement);

        RentalAnnouncementDto Update(RentalAnnouncementDto rentalAnnouncement);

        RentalAnnouncementDto Create(RentalAnnouncementDto rentalAnnouncement);
    }
}
