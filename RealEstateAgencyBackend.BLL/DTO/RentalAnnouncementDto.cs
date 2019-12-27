using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.DTO
{
    public class RentalAnnouncementDto
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String Address { get; set; }

        public Int32 Cost { get; set; }

        public String Location { get; set; }

        public String UserId { get; set; }

        public  ICollection<ImageDto> Images { get; set; }
    }

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
    }
}
