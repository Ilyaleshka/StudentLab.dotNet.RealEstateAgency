using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.DTO
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsActive { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime ReservationTime { get; set; }

        public int RentalAnnouncementId { get; set; }

        public string UserId { get; set; }

    }
}
