using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.DTO;
using System.Security.Claims;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IUserService
    {
        UserDto Find(string userName, string password);
        IdentityResult Create(UserDto user, string password);
        UserDto FindById(string id);
        IdentityResult Delete(UserDto user);
        ClaimsIdentity CreateIdentity(UserDto user, string authenticationTypes);
    }
}
