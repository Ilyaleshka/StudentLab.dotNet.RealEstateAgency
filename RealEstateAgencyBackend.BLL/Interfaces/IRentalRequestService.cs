using RealEstateAgencyBackend.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalRequestService
    {
        IEnumerable<RentalRequestDto> GetAll();

		RentalRequestPageDto GetPageWithFilters(Int32 pageNumber, Int32 pageSize, NameValueCollection filteringParams);

		RentalRequestDto Find(int id);

        RentalRequestDto Remove(RentalRequestDto rentalAnnouncement);

        RentalRequestDto Update(RentalRequestDto rentalAnnouncement);

        RentalRequestDto Create(RentalRequestDto rentalAnnouncement);

    }
}
