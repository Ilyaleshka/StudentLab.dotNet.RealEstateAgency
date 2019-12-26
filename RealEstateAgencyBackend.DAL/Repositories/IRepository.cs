using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public interface IRepository<K, T> where T : class
    {
        IQueryable<T> GetAll();
        T Find(K id);
        T Create(T item);
        T Update(T item);
        T Remove(K id);
    }

}
