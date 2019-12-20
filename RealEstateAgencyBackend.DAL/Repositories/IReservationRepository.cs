using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public interface IReservationRepository: IRepository<int,Reservation>
    {
    }
}
