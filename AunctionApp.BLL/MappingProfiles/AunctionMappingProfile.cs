using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;

namespace AunctionApp.BLL.MappingProfiles
{
    public class AunctionMappingProfile : Profile
    {
        public AunctionMappingProfile()
        {
            CreateMap<AunctionVM, Product>();
            CreateMap<Product, AunctionVM>();
        }
    }
}
