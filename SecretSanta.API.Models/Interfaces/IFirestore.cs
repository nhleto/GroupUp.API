using System.Collections.Generic;

namespace SecretSanta.API.Models.Interfaces
{
    public interface IFirestore<T>
    {
        T Get(T record);
        IEnumerable<T> GetAll();
        T Add(T record);
        bool Update(T record);
        bool Delete(T record);
    }
}