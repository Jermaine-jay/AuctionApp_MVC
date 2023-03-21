using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.MappingProfiles
{
    public class AunctionMappingProfile : Profile
    {
        public AunctionMappingProfile()
        {
            CreateMap<CreateAunctionVM, Product>();
            //CreateMap<UpdateAunctionVM, Product>();

        }
    }
}
