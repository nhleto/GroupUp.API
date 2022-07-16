using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using GroupUp.API.Domain.DTO;
using GroupUp.API.Domain.Models;

namespace GroupUp.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> Create(User user);
        Task<WriteResult> Update(User record);
        Task<bool> Delete(User record);
        Task<User> Get(User record);
        Task<IEnumerable<User>> GetAll();
        Task<List<User>> QueryRecords(Query query);
        Task BatchUpdate(IEnumerable<User> users, Group group);
        Task NukeUsers();
    }
}
