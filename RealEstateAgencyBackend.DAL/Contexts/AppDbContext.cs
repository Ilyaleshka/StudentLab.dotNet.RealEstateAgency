using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateAgencyBackend.Models;
using System.Data.Entity;

namespace RealEstateAgencyBackend.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<RentalAnnouncement> RentalAnnouncements { get; set; }
        public DbSet<RentalRequest> RentalRequests { get; set; }

        public AppDbContext() : base("name=IdentityDb") { }

        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new IdentityDbInit());
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppDbContext>//DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            context.RentalAnnouncements.Add(new RentalAnnouncement() { Title = "title", Description = "description", Area = 50, Address = "Minsk", Cost = 100, Location = "location" });
            context.RentalAnnouncements.Add(new RentalAnnouncement() { Title = "title", Description = "description2", Area = 50, Address = "Minsk", Cost = 100, Location = "location2" });

            context.RentalRequests.Add(new RentalRequest() { Title = "title", Area = 50, PrefferedAddress = "Minsk", MaxPrice = 100 });
            context.RentalRequests.Add(new RentalRequest() { Title = "title2", Area = 150, PrefferedAddress = "Grodno", MaxPrice = 1000 });

            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(AppDbContext context)
        {
            
        }
    }
}
