using System.Collections.Generic;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public interface IRepository<K, T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(K id);
        void Create(T item);
        void Update(T item);
        T Remove(K id);
    }

}
