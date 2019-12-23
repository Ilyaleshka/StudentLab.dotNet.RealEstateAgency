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
using Microsoft.AspNet.Identity;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalRequestService : IRentalRequestService
    {
        private IUnitOfWork _dal;
        private IRentalRequestRepository _repository;
        private IMapper _mapper;

        private UserManager<User> _userManager;

        public RentalRequestService(IUnitOfWork dal, IMapper mapper)
        {
            _dal = dal;
            _repository = this._dal.RentalRequestRepository;
            _mapper = mapper;
            _userManager = new UserManager<User>(_dal.UserRepository);
        }

        public void Create(RentalRequestDto rentalRequestDto)
        {
            RentalRequest rentalRequest = _mapper.Map<RentalRequest>(rentalRequestDto);

            User user = _userManager.FindById(rentalRequestDto.UserId);
            rentalRequest.User = user;

            _repository.Create(rentalRequest);
            _dal.Save();
        }

        public RentalRequestDto Find(int id)
        {
            RentalRequest rentalRequest = _repository.Find(id);
            RentalRequestDto rentalRequestDto = _mapper.Map<RentalRequestDto>(rentalRequest);

            return rentalRequestDto;
        }

        public IEnumerable<RentalRequestDto> GetAll()
        {
            List<RentalRequestDto> rentalRequestDtos = _mapper.Map<IEnumerable<RentalRequest>, List<RentalRequestDto>>(_repository.GetAll());

            return rentalRequestDtos;
        }

        public RentalRequestDto Remove(RentalRequestDto rentalRequest)
        {
            var temp = _repository.Remove(rentalRequest.Id);
            _dal.Save();

            RentalRequestDto rentalRequestDto = _mapper.Map<RentalRequestDto>(temp);

            return rentalRequestDto;
        }

        public void Update(RentalRequestDto rentalRequestDto)
        {
            RentalRequest rentalRequest = _mapper.Map<RentalRequest>(rentalRequestDto);
            _repository.Update(rentalRequest);
            _dal.Save();
        }

    }
}
