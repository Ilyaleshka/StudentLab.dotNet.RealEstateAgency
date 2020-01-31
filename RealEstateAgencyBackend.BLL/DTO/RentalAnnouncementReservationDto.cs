using System;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.DTO
{
	public class RentalAnnouncementReservationDto
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String Address { get; set; }

        public Int32 Cost { get; set; }

        public String Location { get; set; }

        public String UserId { get; set; }

        public ICollection<ImageDto> Images { get; set; }

		public ReservationDto Reservation { get; set; }
    }
}
