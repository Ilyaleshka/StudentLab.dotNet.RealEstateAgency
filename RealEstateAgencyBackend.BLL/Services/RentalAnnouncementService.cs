using AutoMapper;
using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using RealEstateAgencyBackend.Models;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalAnnouncementService : IRentalAnnouncementService
    {
        private IUnitOfWork _dal;
        private IRentalAnnouncementRepository _repository;

        private UserManager<User> _userManager;

        public RentalAnnouncementService(IUnitOfWork dal)
        {
            _dal = dal;
            _repository = _dal.RentalAnnouncementRepository;

            _userManager = new UserManager<User>(_dal.UserRepository);
        }

        public void Create(RentalAnnouncementDto rentalAnnouncementDto)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalAnnouncementDto, RentalAnnouncement>());
            var mapper = config.CreateMapper();
            RentalAnnouncement rentalAnnouncement = mapper.Map<RentalAnnouncement>(rentalAnnouncementDto);

            User user = _userManager.FindById(rentalAnnouncementDto.UserId);
            rentalAnnouncement.User = user;

            _repository.Create(rentalAnnouncement);
            _dal.Save();
        }

        public RentalAnnouncementDto Find(int id)
        {
            RentalAnnouncement rentalAnnouncement = _repository.Find(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalAnnouncement, RentalAnnouncementDto>());
            var mapper = config.CreateMapper();
            RentalAnnouncementDto rentalAnnouncementDto = mapper.Map<RentalAnnouncementDto>(rentalAnnouncement);

            return rentalAnnouncementDto;
        }

        public IEnumerable<RentalAnnouncementDto> GetAll()
        {
            //IEnumerable<RentalAnnouncement> rentalAnnouncements = _repository.GetAll();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalAnnouncement, RentalAnnouncementDto>());
            var mapper = config.CreateMapper();
            List<RentalAnnouncementDto> rentalAnnouncementDtos = mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementDto>>(_repository.GetAll());

            return rentalAnnouncementDtos;
        }

        public RentalAnnouncementDto Remove(RentalAnnouncementDto rentalAnnouncement)
        {
            var temp =  _repository.Remove(rentalAnnouncement.Id);
            _dal.Save();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalAnnouncement, RentalAnnouncementDto>());
            var mapper = config.CreateMapper();
            RentalAnnouncementDto rentalAnnouncementDto = mapper.Map<RentalAnnouncementDto>(temp);

            return rentalAnnouncementDto;
        }

        public void Update(RentalAnnouncementDto rentalAnnouncementDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalAnnouncementDto, RentalAnnouncement>());
            var mapper = config.CreateMapper();
            RentalAnnouncement rentalAnnouncement = mapper.Map<RentalAnnouncement>(rentalAnnouncementDto);

            _repository.Update(rentalAnnouncement);
            _dal.Save();
        }

        //RentalAnnouncement rentalAnnouncement = db.RentalAnnouncements.Find(id);
    }
}
