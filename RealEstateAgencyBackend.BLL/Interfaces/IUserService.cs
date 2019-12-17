using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.Models;
using System.Security.Claims;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IUserService
    {
        User Find(string userName, string password);
        IdentityResult Create(User user, string password);
        User FindById(string id);
        IdentityResult Delete(User user);
        ClaimsIdentity CreateIdentity(User user, string authenticationTypes);
    }
}
