using AutoMapper;
using FirebaseAdmin.Auth;
using SecretSanta.API.Domain.DTO;

namespace SecretSanta.API.Firestore.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRecord, UserDto>();
        }
    }
}