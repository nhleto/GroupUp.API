using System.Linq;
using System.Threading.Tasks;
using GroupUp.API.Domain.Interfaces;
using GroupUp.API.Domain.Models;

namespace GroupUp.API.Firestore.Workflows
{
    public class GroupCreationWorkflow : IGroupCreationWorkflow
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        
        public GroupCreationWorkflow(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task AddGroupsToUser(Group group)
        {
            var users = group.Users.Where(u => u.Id != null).ToArray();
            await _userRepository.BatchUpdate(users, group);
        }
    }
}