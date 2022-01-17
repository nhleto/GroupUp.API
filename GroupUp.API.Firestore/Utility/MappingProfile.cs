using AutoMapper;
using FirebaseAdmin.Auth;
using GroupUp.API.Domain.DTO;
using GroupUp.API.Domain.Models;

namespace GroupUp.API.Firestore.Utility
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