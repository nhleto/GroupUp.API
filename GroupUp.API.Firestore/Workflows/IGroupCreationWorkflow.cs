using System.Threading.Tasks;
using GroupUp.API.Domain.Models;

namespace GroupUp.API.Firestore.Workflows
{
    public interface IGroupCreationWorkflow
    {
        Task AddGroupsToUser(Group group);
    }
}