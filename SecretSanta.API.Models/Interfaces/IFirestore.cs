using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.API.Models.Interfaces
{
    public interface IFirestore<in T>
    {
        Task<User> Get(T record);
        Task<IEnumerable<User>> GetAll();
        Task<User> Add(T record);
        Task<bool> Update(T record);
        Task<bool> Delete(T record);
    }
}