using RealEstateAgencyBackend.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalAnnouncementService
    {
        IEnumerable<RentalAnnouncementDto> GetAll();

		RentalAnnouncementPageDto GetPageWithFilters(Int32 pageNumber, Int32 pageSize, NameValueCollection filteringParams);

		RentalAnnouncementDto Find(int id);

		RentalAnnouncementReservationDto GetFullInfo(int id);

		RentalAnnouncementDto Remove(RentalAnnouncementDto rentalAnnouncement);

        RentalAnnouncementDto Update(RentalAnnouncementDto rentalAnnouncement);

        RentalAnnouncementDto Create(RentalAnnouncementDto rentalAnnouncement);
    }
}
