using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;

namespace AunctionApp.BLL.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();

            CreateMap<User, RegisterVM>();
            CreateMap<RegisterVM, User>();
        }
    }
}
