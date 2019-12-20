using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.Models
{
    public class User : IdentityUser
    {
        public String UserLastName { get; set; }

        public virtual ICollection<RentalAnnouncement> RentalAnnouncements { get; set; }

        public virtual ICollection<RentalRequest> RentalRequests { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public User()
        {
            RentalAnnouncements = new List<RentalAnnouncement>();
            RentalRequests = new List<RentalRequest>();
            Reservations = new List<Reservation>(); 
        }
    }
}
