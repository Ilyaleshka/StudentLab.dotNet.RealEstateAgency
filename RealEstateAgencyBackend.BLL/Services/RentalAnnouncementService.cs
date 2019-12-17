using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using RealEstateAgencyBackend.Models;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalAnnouncementService : IRentalAnnouncementService
    {
        private IUnitOfWork Dal;
        private IRentalAnnouncementRepository repository;

        public RentalAnnouncementService(IUnitOfWork dal = null)
        {
            Dal = new UnitOfWork();
            repository = Dal.RentalAnnouncementRepository;
        }

        public void Create(RentalAnnouncement rentalAnnouncement)
        {
            repository.Create(rentalAnnouncement);
            Dal.Save();
        }

        public RentalAnnouncement Find(int id)
        {
            return repository.Find(id);
        }

        public IEnumerable<RentalAnnouncement> GetAll()
        {
            return repository.GetAll();
        }

        public RentalAnnouncement Remove(RentalAnnouncement rentalAnnouncement)
        {
            var temp =  repository.Remove(rentalAnnouncement.Id);
            Dal.Save();
            return temp;
        }

        public void Update(RentalAnnouncement rentalAnnouncement)
        {
            repository.Update(rentalAnnouncement);
            Dal.Save();
        }

        //RentalAnnouncement rentalAnnouncement = db.RentalAnnouncements.Find(id);
    }
}
