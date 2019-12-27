using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private AppDbContext _context;

        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public PostImage Create(PostImage item)
        {
            return _context.Images.Add(item);
        }

        public PostImage Find(int id)
        {
            return _context.Images.Find(id);
        }

        public IQueryable<PostImage> GetAll()
        {
            return _context.Images;
        }

        public PostImage Remove(int id)
        {
            PostImage img = _context.Images.Find(id);
            if (img != null)
                img = _context.Images.Remove(img);
            return img;
        }

        public PostImage Update(PostImage item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }
    }
}
