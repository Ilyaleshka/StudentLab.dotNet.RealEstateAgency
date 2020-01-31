using System;

namespace RealEstateAgencyBackend.Models
{
	public class RentalRequestViewModel
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String PrefferedAddress { get; set; }

        public Int32 MaxPrice { get; set; }

        public String UserId { get; set; }
    }
}