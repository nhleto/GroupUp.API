using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.API.Models.Interfaces
{
    public interface IFirestore<T>
    {
        Task<T> Get(T record);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T record);
        Task<bool> Update(T record);
        Task<bool> Delete(T record);
    }
}