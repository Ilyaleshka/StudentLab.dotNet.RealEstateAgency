using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.Models
{
    public class PostImage
    {
		[Key()]
		public int Id { get; set; }

		public String ImagePath { get; set; }

        public int? RentalAnnouncementId { get; set; }

        public virtual RentalAnnouncement RentalAnnouncement { get; set; }
    }
}
