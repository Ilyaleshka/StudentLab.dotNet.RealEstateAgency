using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.Models;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public interface IUserRepository : IRepository<string, User>, IUserStore<User>
    { }
}
