using RealEstateAgencyBackend.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalRequestService
    {
        IEnumerable<RentalRequestDto> GetAll();

        RentalRequestDto Find(int id);

        RentalRequestDto Remove(RentalRequestDto rentalAnnouncement);

        void Update(RentalRequestDto rentalAnnouncement);

        void Create(RentalRequestDto rentalAnnouncement);

    }
}
