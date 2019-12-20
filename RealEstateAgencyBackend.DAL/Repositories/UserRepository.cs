using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            CreateAsync(item);
        }

        public User Find(string id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User Remove(string id)
        {
            User user = _context.Users.Find(id);
            if(user != null)
                user = _context.Users.Remove(user);
            return user;
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

    }
}
