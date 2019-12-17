using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IRentalRequestService
    {
        IEnumerable<RentalRequest> GetAll();
        RentalRequest Find(int id);

        RentalRequest Remove(RentalRequest rentalAnnouncement);

        void Update(RentalRequest rentalAnnouncement);

        void Create(RentalRequest rentalAnnouncement);

    }
}
