using AutoMapper;
using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using RealEstateAgencyBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalAnnouncementService : IRentalAnnouncementService
    {
        private IUnitOfWork _dal;
        private IRentalAnnouncementRepository _repository;
        private IMapper _mapper;

        private UserManager<User> _userManager;

        public RentalAnnouncementService(IUnitOfWork dal, IMapper mapper)
        {
            _dal = dal;
            _repository = _dal.RentalAnnouncementRepository;
            _mapper = mapper;
            _userManager = new UserManager<User>(_dal.UserRepository);
        }

        public RentalAnnouncementDto Create(RentalAnnouncementDto rentalAnnouncementDto)
        {
            RentalAnnouncement rentalAnnouncement = _mapper.Map<RentalAnnouncement>(rentalAnnouncementDto);

            User user = _userManager.FindById(rentalAnnouncementDto.UserId);
            rentalAnnouncement.User = user;

            _repository.Create(rentalAnnouncement);
            _dal.Save();

            return _mapper.Map<RentalAnnouncementDto>(rentalAnnouncement);
        }

        public RentalAnnouncementDto Find(int id)
        {
            RentalAnnouncement rentalAnnouncement = _repository.Find(id);
            RentalAnnouncementDto rentalAnnouncementDto = _mapper.Map<RentalAnnouncementDto>(rentalAnnouncement);

            return rentalAnnouncementDto;
        }

        //

        public IEnumerable<RentalAnnouncementDto> GetAll()
        {
            IEnumerable<RentalAnnouncement> announcements = _repository.GetAll();

            announcements = announcements.Where(announcement => announcement.Reservations
                    .All(reservation => (!reservation.IsActive && reservation.IsConfirmed)));

            List<RentalAnnouncementDto> rentalAnnouncementDtos = _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementDto>>(announcements);

            return rentalAnnouncementDtos;
        }

        public RentalAnnouncementDto Remove(RentalAnnouncementDto rentalAnnouncement)
        {
            var temp =  _repository.Remove(rentalAnnouncement.Id);
            _dal.Save();

            RentalAnnouncementDto rentalAnnouncementDto = _mapper.Map<RentalAnnouncementDto>(temp);
            return rentalAnnouncementDto;
        }

        public RentalAnnouncementDto Update(RentalAnnouncementDto rentalAnnouncementDto)
        {
            RentalAnnouncement rentalAnnouncement = _mapper.Map<RentalAnnouncement>(rentalAnnouncementDto);
            _repository.Update(rentalAnnouncement);
            _dal.Save();

            return _mapper.Map<RentalAnnouncementDto>(rentalAnnouncement);
        }
    }
}
