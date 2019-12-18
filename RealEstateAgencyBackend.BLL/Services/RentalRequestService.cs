using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgencyBackend.BLL.DTO;
using AutoMapper;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalRequestService : IRentalRequestService
    {
        private IUnitOfWork _dal;
        private IRentalRequestRepository _repository;

        public RentalRequestService(IUnitOfWork dal = null)
        {
            this._dal = dal;
            _repository = this._dal.RentalRequestRepository;
        }

        public void Create(RentalRequestDto rentalRequestDto)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalRequestDto, RentalRequest>());
            var mapper = config.CreateMapper();
            RentalRequest rentalRequest = mapper.Map<RentalRequest>(rentalRequestDto);

            _repository.Create(rentalRequest);
            _dal.Save();
        }

        public RentalRequestDto Find(int id)
        {
            RentalRequest rentalRequest = _repository.Find(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalRequest, RentalRequestDto>());
            var mapper = config.CreateMapper();
            RentalRequestDto rentalRequestDto = mapper.Map<RentalRequestDto>(rentalRequest);

            return rentalRequestDto;
        }

        public IEnumerable<RentalRequestDto> GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalRequest, RentalRequestDto>());
            var mapper = config.CreateMapper();
            List<RentalRequestDto> rentalRequestDtos = mapper.Map<IEnumerable<RentalRequest>, List<RentalRequestDto>>(_repository.GetAll());

            return rentalRequestDtos;
        }

        public RentalRequestDto Remove(RentalRequestDto rentalRequest)
        {
            var temp = _repository.Remove(rentalRequest.Id);
            _dal.Save();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalRequest, RentalRequestDto>());
            var mapper = config.CreateMapper();
            RentalRequestDto rentalRequestDto = mapper.Map<RentalRequestDto>(temp);

            return rentalRequestDto;
        }

        public void Update(RentalRequestDto rentalRequestDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalRequestDto, RentalRequest>());
            var mapper = config.CreateMapper();
            RentalRequest rentalRequest = mapper.Map<RentalRequest>(rentalRequestDto);

            _repository.Update(rentalRequest);
            _dal.Save();
        }

    }
}
