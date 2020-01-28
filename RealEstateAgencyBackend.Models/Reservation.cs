using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public bool IsConfirmed { get; set; }

		public bool IsRejected { get; set; }

		public bool IsActive { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime ReservationTime { get; set; }

        public int RentalAnnouncementId { get; set; }

        public virtual RentalAnnouncement RentalAnnouncement { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}
