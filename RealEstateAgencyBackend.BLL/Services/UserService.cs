using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System.Security.Claims;
using RealEstateAgencyBackend.BLL.DTO;
using AutoMapper;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _dal;

        private UserManager<User> userManager;

        public UserService(IUnitOfWork dal)
        {
            _dal = dal;
            userManager = new UserManager<User>(_dal.UserRepository);
            ConfigureUserManager(userManager);
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
                RequireUniqueEmail = true,  //<-- the default is true,
            };
        }

        public IdentityResult Create(UserDto userDto, string password)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
            var mapper = config.CreateMapper();
            User user = mapper.Map<User>(userDto);

            return userManager.Create(user, password);
        }

        public ClaimsIdentity CreateIdentity(UserDto userDto, string authenticationTypes)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
            var mapper = config.CreateMapper();
            User user = mapper.Map<User>(userDto);

            return userManager.CreateIdentity(user, authenticationTypes);
        }

        public IdentityResult Delete(UserDto userDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
            var mapper = config.CreateMapper();
            User user = mapper.Map<User>(userDto); 

            return userManager.Delete(user);
        }

        public UserDto Find(string userName, string password)
        {
            User user = userManager.Find(userName, password);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            var mapper = config.CreateMapper();
            UserDto userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        public UserDto FindById(string id)
        {
            User user = userManager.FindById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            var mapper = config.CreateMapper();
            UserDto userDto = mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
