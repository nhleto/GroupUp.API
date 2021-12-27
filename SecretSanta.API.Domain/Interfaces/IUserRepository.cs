using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> SignUp(User record);
        public Task<bool> Update(User record);
        public Task<bool> Delete(User record);
        public Task<User> Get(User record);
        public Task<IEnumerable<User>> GetAll();
        public Task<List<User>> QueryRecords(Query query);
    }
}