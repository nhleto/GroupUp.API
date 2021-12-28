using System;
using System.Threading.Tasks;
using AutoMapper;
using SecretSanta.API.Domain.DTO;
using SecretSanta.API.Domain.Interfaces;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Firestore.Workflows
{
    public class UserWorkflow : IUserWorkflow
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserWorkflow(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleSignIn(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Invalid login credentials");
            }

            var userRecord = await _userRepository.FindUserByEmail(user);

            if (userRecord == null)
            {
                throw new Exception("User not found");
            }
            
            return _mapper.Map<UserDto>(userRecord);
        }
    }
}