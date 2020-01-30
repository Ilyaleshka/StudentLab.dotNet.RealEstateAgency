using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyBackend.Models
{
    public class RentalAnnouncementCreateModel
    {
        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String Address { get; set; }

        public Int32 Cost { get; set; }

        public String Location { get; set; }

        public String[] Base64Images { get; set; }
    }

    public class RentalAnnouncementViewModel
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String Address { get; set; }

        public Int32 Cost { get; set; }

        public String Location { get; set; }

		public ICollection<String> Images { get; set; }
	}

    public class RentalAnnouncementReservationViewModel
    {
		public int Id { get; set; }

		public String Title { get; set; }

		public String Description { get; set; }

		public Int32 Area { get; set; }

		public String Address { get; set; }

		public Int32 Cost { get; set; }

		public String Location { get; set; }

		public String UserId { get; set; }

		public ICollection<String> Images { get; set; }

		public ReservationViewModel Reservation { get; set; }
	}

	public class ReservationViewModel
	{
		public int Id { get; set; }

		public bool IsConfirmed { get; set; }

		public bool IsRejected { get; set; }

		public bool IsActive { get; set; }

		public int RentalAnnouncementId { get; set; }

		public string UserId { get; set; }
	}

	public class RentalAnnouncementPageView
	{
		public List<RentalAnnouncementViewModel> RentalAnnouncements { get; set; }
		public Int32 PageCount { get; set; }
		public Int32 CurrentPage { get; set; }
	}
}