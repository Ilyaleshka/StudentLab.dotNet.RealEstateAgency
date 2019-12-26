using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.Models
{
    public class PostImage
    {
        public int Id { get; set; }

        public String ImagePath { get; set; }

        public int RentalAnnouncementId { get; set; }

        public virtual RentalAnnouncement RentalAnnouncement { get; set; }
    }
}
