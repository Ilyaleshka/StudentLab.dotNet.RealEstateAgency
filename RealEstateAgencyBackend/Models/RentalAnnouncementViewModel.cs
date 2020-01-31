using System;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.Models
{
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
}