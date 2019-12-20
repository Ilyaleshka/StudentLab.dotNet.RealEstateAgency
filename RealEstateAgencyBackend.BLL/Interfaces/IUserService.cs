using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.DTO;
using System.Collections.Generic;
using System.Security.Claims;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IUserService
    {
        UserDto Find(string userName, string password);
        UserDto FindById(string id);

        bool IsUserExist(string userName);
        string GetUserId(string userName);

        IdentityResult Create(CreateUserDto user);
        IdentityResult Delete(string id);

        ClaimsIdentity CreateIdentity(string userName, string password, string authenticationTypes);

        IEnumerable<RentalAnnouncementDto> GetRentalAnnouncements(string userId);
        IEnumerable<RentalRequestDto> GetRentalRequests(string userId);
        IEnumerable<RentalAnnouncementDto> GetReservations(string userId);

        bool ReserveAnnouncement(int announcementId, string userId);
        void UnreserveAnnouncement(int announcementId, string userId);

        void ConfirmReservation(int announcementId, string userId);
        void DeleteReservation(int announcementId, string userId);
    }
}
