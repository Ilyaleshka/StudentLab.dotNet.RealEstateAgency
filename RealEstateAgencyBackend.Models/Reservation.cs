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
        [ForeignKey("RentalAnnouncement")]
        public int Id { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime ReservationTime { get; set; }

        public virtual RentalAnnouncement RentalAnnouncement { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
