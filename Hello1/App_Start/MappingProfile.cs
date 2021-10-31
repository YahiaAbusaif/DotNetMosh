using AutoMapper;
using Hello1.Dtos;
using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //domin to dto 
            Mapper.CreateMap<Customer,CustomerDto>();
            Mapper.CreateMap<Movie,MovieDto>();
            Mapper.CreateMap<MembershipType,MembershipDto>();

            //dto to domin
            Mapper.CreateMap<CustomerDto,Customer>();
            Mapper.CreateMap<MovieDto,Movie>();
            Mapper.CreateMap<MembershipDto,MembershipType>();
        }
    }
}