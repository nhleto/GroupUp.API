using System.Collections.Generic;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Domain.DTO
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        
        public string Email { get; set; }
        
        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<string> WishList { get; set; }
    }
}