using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.DAL.Repositories
{

    public class UserRepository : UserStore<User>, IUserRepository
    {
        private AppDbContext _context;

        public UserRepository(AppDbContext userContext)
            : base(userContext)
        {
            _context = userContext;
        }

        public void Create(User item)
        {
            throw new NotImplementedException();
        }


        public User Find(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }

    }
}
