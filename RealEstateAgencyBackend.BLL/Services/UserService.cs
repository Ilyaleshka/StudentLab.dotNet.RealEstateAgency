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
            // As I see you initialized validator in AppUserManager. Why do you need to do it here?
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

        public bool IsUserExist(string userName)
        {
            return (_userManager.FindByName(userName) == null) ? false : true;
        }

        public string GetUserId(string userName)
        {
            User user = _userManager.FindByName(userName);
            return _userManager.FindByName(userName)?.Id;
        }



		public IEnumerable<RentalAnnouncementReservationDto> GetRentalAnnouncements(string id)
		{
			User user = _userManager.FindById(id);
			return _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementReservationDto>>(user.RentalAnnouncements);
		}


		public IEnumerable<RentalRequestDto> GetRentalRequests(string id)
		{
			User user = _userManager.FindById(id);
			return _mapper.Map<IEnumerable<RentalRequest>, List<RentalRequestDto>>(user.RentalRequests);
		}


		public IEnumerable<RentalAnnouncementReservationDto> GetReservations(string userId)
		{
			User user = _userManager.FindById(userId);
			var reservations = _dal.RentalAnnouncementRepository.GetAll()
				.Where(announcement => (announcement.Reservations.Count > 0) && announcement.Reservations.Any(reservation => (reservation.UserId == userId)
							&&((!reservation.IsActive && !reservation.IsConfirmed && !reservation.IsRejected)  || (reservation.IsActive && reservation.IsConfirmed))));
			List<RentalAnnouncement> userReservations = new List<RentalAnnouncement>(reservations);

			return _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementReservationDto>>(userReservations);
		}




        public bool ReserveAnnouncement(int announcementId, string userId)
        {
            User user = _userManager.FindById(userId);
            RentalAnnouncement rentalAnnouncement = _dal.RentalAnnouncementRepository.Find(announcementId);

            if (user == null || rentalAnnouncement == null || rentalAnnouncement.Reservations.Any(r => r.IsActive || (!r.IsActive && !r.IsConfirmed && !r.IsRejected)) )
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
            // Probably is makes sense to return resevation?
            return true;
        }

        public void CompliteReservation(int announcementId, string userId)
        {
            IEnumerable<Reservation> reservs = _dal.ReservationRepository.GetAll()
                .Where(res => (res.RentalAnnouncement.UserId == userId) && (res.RentalAnnouncementId == announcementId) && (res.IsActive && res.IsConfirmed));

			if (reservs.Any())
			{
				var reserv = reservs.First();
				reserv.IsActive = false;
				reserv.EndTime = DateTime.Now;
			}

            _dal.Save();
        }

        public void ConfirmReservation(int announcementId, string ownerId)
        {
            IEnumerable<Reservation> reservs = _dal.ReservationRepository.GetAll()
                .Where(res => res.RentalAnnouncement.UserId == ownerId && res.RentalAnnouncementId == announcementId && (!res.IsConfirmed && !res.IsRejected));

			if (reservs.Any())
			{
				var reserv = reservs.First();
				reserv.IsConfirmed = true;
				reserv.IsActive = true;
			}

			_dal.Save();
        }

		public void RejectReservation(int announcementId, string ownerId)
		{
			IEnumerable<Reservation> reservs = _dal.ReservationRepository.GetAll()
				.Where(res => (res.RentalAnnouncement.UserId == ownerId) && (res.RentalAnnouncementId == announcementId) && (!res.IsConfirmed && !res.IsRejected && !res.IsRejected));

			if (reservs.Any())
			{
				var reserv = reservs.First();
				reserv.IsRejected = true;
			}

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

		public IEnumerable<UserDto> GetAll()
		{
			List<UserDto> users  = _mapper.Map<IEnumerable<User>, List<UserDto>>(_userManager.Users);
			return users;
		}
	}
}
