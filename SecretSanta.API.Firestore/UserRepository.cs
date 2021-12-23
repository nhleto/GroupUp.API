using System.Collections.Generic;
using System.Threading.Tasks;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;

namespace SecretSanta.API.Firestore
{
    public class UserRepository : IFirestore<User>
    {
        private readonly BaseRepository _baseRepository;

        public UserRepository(BaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<User> Add(User record) => _baseRepository.Add(record);
        public Task<bool> Update(User record) => _baseRepository.Update(record);
        public Task<bool> Delete(User record) => _baseRepository.Delete(record);
        public Task<User> Get(User record) => _baseRepository.Get(record);
        public Task<IEnumerable<User>> GetAll() => _baseRepository.GetAll<User>();
    }
}