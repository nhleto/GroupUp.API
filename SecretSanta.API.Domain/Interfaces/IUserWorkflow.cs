using System.Threading.Tasks;
using SecretSanta.API.Domain.DTO;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Domain.Interfaces
{
    public interface IUserWorkflow
    {
        public Task<UserDto> HandleSignIn(User user);
    }
}