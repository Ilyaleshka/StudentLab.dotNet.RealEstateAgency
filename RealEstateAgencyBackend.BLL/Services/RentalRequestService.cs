using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalRequestService : IRentalRequestService
    {
        private IUnitOfWork Dal;
        private IRentalRequestRepository repository;

        public RentalRequestService(IUnitOfWork dal = null)
        {
            Dal = dal;
            repository = Dal.RentalRequestRepository;
        }

        public RentalRequest Find(int id)
        {
            return repository.Find(id);
        }

        public IEnumerable<RentalRequest> GetAll()
        {
            return repository.GetAll();
        }


        public void Create(RentalRequest rentalRequest)
        {
            repository.Create(rentalRequest);
            Dal.Save();
        }

        public RentalRequest Remove(RentalRequest rentalRequest)
        {
            var temp = repository.Remove(rentalRequest.Id);
            Dal.Save();
            return temp;
        }

        public void Update(RentalRequest rentalRequest)
        {
            repository.Update(rentalRequest);
            Dal.Save();
        }
    }
}
