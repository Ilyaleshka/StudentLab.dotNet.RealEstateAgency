using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgencyBackend.Models
{
    public class RentalAnnouncement
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String Address { get; set; }

        public Int32 Cost { get; set; }

        public String Location { get; set; }

        public String UserId { get; set; }

        public User User { get; set; }
    }
}
