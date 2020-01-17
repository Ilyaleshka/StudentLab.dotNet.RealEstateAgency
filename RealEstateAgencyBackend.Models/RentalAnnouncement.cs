using System;
using System.Collections.Generic;
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

        public virtual User User { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<PostImage> PostImages { get; set; }

		public RentalAnnouncement()
		{
			PostImages = new List<PostImage>();
			Reservations = new List<Reservation>();
		}
	}
}
