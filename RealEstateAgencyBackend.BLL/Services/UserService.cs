using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System.Security.Claims;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Dal;

        private UserManager<User> userManager;

        public UserService(IUnitOfWork dal)
        {
            Dal = dal;
            userManager = new UserManager<User>(Dal.UserRepository);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };
        }

        public IdentityResult Create(User user, string password)
        {
            return userManager.Create(user, password);
        }

        public ClaimsIdentity CreateIdentity(User user, string authenticationTypes)
        {
            return userManager.CreateIdentity(user, authenticationTypes);
        }

        public IdentityResult Delete(User user)
        {
            return userManager.Delete(user);
        }

        public User Find(string userName, string password)
        {
            return userManager.Find(userName, password);
        }

        public User FindById(string id)
        {
            return userManager.FindById(id);
        }
    }
}
