using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace SecretSanta.API.Models.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Add(User record);
        public Task<bool> Update(User record);
        public Task<bool> Delete(User record);
        public Task<User> Get(User record);
        public Task<IEnumerable<User>> GetAll();
        public Task<List<User>> QueryRecords(Query query);
    }
}