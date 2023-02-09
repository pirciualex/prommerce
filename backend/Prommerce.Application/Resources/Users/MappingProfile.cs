using AutoMapper;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Data.Entites;

namespace Prommerce.Application.Resources.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserPostDto, User>();
            CreateMap<User, UserGetDto>();
            CreateMap<UserPutDto, User>();
        }
    }
}