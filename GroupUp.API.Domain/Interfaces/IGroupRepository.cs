using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using GroupUp.API.Domain.Models;

namespace GroupUp.API.Domain.Interfaces
{
    public interface IGroupRepository
    {
        public Task<Group> Add(Group record);
        public Task<bool> Update(Group record);
        public Task<bool> Delete(Group record);
        public Task<Group> Get(Group record);
        public Task<IEnumerable<Group>> GetAll();
        public Task<IEnumerable<Group>> QueryRecords(Query query);
    }
}