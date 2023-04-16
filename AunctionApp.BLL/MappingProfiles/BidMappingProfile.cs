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
    public class BidMappingProfile : Profile
    {
        public BidMappingProfile() 
        {
            CreateMap<AddOrUpdateBidVM, Bid>();
            CreateMap<AddOrUpdateBidVM, Bid>().ReverseMap();
        }
    }
}
