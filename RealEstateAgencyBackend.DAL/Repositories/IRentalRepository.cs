using System.Collections.Generic;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public interface IRentalRepository<T> : IRepository<int, T> where T : class
    {
        IEnumerable<T> GetByUserId(string userID);
    }

}
