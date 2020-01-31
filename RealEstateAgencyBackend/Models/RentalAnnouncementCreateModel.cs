using System;

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
}