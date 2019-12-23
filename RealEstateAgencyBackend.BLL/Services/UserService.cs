using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System.Security.Claims;
using RealEstateAgencyBackend.BLL.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _dal;
        private IMapper _mapper;

        private UserManager<User> _userManager;

        public UserService(IUnitOfWork dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;

            _userManager = new UserManager<User>(_dal.UserRepository);
            ConfigureUserManager(_userManager);
        }

        private void ConfigureUserManager(UserManager<User> userManager)
        {
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            userManager.UserValidator = new UserValidator<User>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };
        }


        public IdentityResult Create(CreateUserDto userDto)
        {
            User user = new User { UserName = userDto.UserName, Email = userDto.Email, UserLastName = userDto.UserLastName };

            IdentityResult result =  _userManager.Create(user, userDto.Password);
            _dal.Save();

            return result;
        }

        public IdentityResult Delete(string id)
        {
            User user = _userManager.FindById(id);

            IdentityResult result = _userManager.Delete(user);
            _dal.Save();

            return result;
        }


        public UserDto Find(string userName, string password)
        {
            User user = _userManager.Find(userName, password);
            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public UserDto FindById(string id)
        {
            User user = _userManager.FindById(id);
            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public ClaimsIdentity CreateIdentity(string userName, string password, string authenticationTypes)
        {
            User user = _userManager.Find(userName, password);

            return _userManager.CreateIdentity(user, authenticationTypes);
        }



        public IEnumerable<RentalAnnouncementDto> GetRentalAnnouncements(string id)
        {
            User user = _userManager.FindById(id);
            return _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementDto>>(user.RentalAnnouncements);

        }

        public IEnumerable<RentalRequestDto> GetRentalRequests(string id)
        {
            User user = _userManager.FindById(id);
            return _mapper.Map<IEnumerable<RentalRequest>, List<RentalRequestDto>>(user.RentalRequests);
        }


        public bool IsUserExist(string userName)
        {
            return (_userManager.FindByName(userName) == null) ? false : true;
        }

        public string GetUserId(string userName)
        {
            User user = _userManager.FindByName(userName);
            return _userManager.FindByName(userName)?.Id;
        }

        public IEnumerable<RentalAnnouncementDto> GetReservations(string userId)
        {
            User user = _userManager.FindById(userId);
            List<RentalAnnouncement> rentalAnnouncements = new List<RentalAnnouncement>(user.RentalAnnouncements);

            return _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementDto>>(rentalAnnouncements.Where(a => a.Reservations.Any(r => r.IsActive)));
        }

        public bool ReserveAnnouncement(int announcementId, string userId)
        {
            User user = _userManager.FindById(userId);
            RentalAnnouncement rentalAnnouncement = _dal.RentalAnnouncementRepository.Find(announcementId);

            if (user == null || rentalAnnouncement == null || rentalAnnouncement.Reservations.Any(r => r.IsActive))
                return false;

            Reservation reservation = new Reservation
            {
                IsConfirmed = false,
                IsActive = false,
                ReservationTime = DateTime.Now,
                User = user,
                RentalAnnouncement = rentalAnnouncement
            };

            _dal.ReservationRepository.Create(reservation);
            _dal.Save();
            return true;
        }

        public void UnreserveAnnouncement(int announcementId, string userId)
        {
            IEnumerable<Reservation> reservs = _dal.ReservationRepository.GetAll()
                .Where(res => res.UserId == userId && res.RentalAnnouncementId == announcementId && (res.IsActive || (!res.IsConfirmed)));

            if (reservs.Any())
                reservs.First().IsActive = false;

            _dal.Save();
        }

        public void ConfirmReservation(int announcementId, string ownerId)
        {
            IEnumerable<Reservation> reservs = _dal.ReservationRepository.GetAll()
                .Where(res => res.RentalAnnouncement.UserId == ownerId && res.RentalAnnouncementId == announcementId && (!res.IsConfirmed));

            if (reservs.Any())
                reservs.First().IsConfirmed = true;

            _dal.Save();
        }

        public void DeleteReservation(int announcementId, string ownerId)
        {
            IEnumerable<Reservation> reservs = _dal.ReservationRepository.GetAll()
                .Where(res => res.RentalAnnouncement.UserId == ownerId && res.RentalAnnouncementId == announcementId && (res.IsActive));

            if (reservs.Any())
            {
                var res = reservs.First();
                res.IsActive = false;
                res.EndTime = DateTime.Now;
            }

            _dal.Save();
        }
    }
}
