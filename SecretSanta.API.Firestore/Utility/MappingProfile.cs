using AutoMapper;
using FirebaseAdmin.Auth;
using SecretSanta.API.Domain.DTO;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Firestore.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRecord, UserDto>();
            CreateMap<User, UserRecordArgs>();
        }
    }
}