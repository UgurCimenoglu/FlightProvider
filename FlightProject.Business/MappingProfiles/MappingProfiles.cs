using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FlightProject.Core.Entities;
using FlightProject.Entities.Concrete;
using FlightProject.Entities.Dtos;

namespace FlightProject.Business.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Flight, FlightAddDto>().ReverseMap();
            CreateMap<Flight, FlightResponseDto>().ReverseMap();
            CreateMap<User, UserDetailDto>().ReverseMap();
            CreateMap<PassengerInformation, FlightPassengerResponseDto>().ReverseMap();
            CreateMap<PassengerInformation, FlightPassengerAddDto>().ReverseMap();
            CreateMap<Flight, FlightPassengerAddDto>().ReverseMap();
        }
    }
}
