using AutoMapper;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class CountyProfile : Profile
    {
        public CountyProfile()
        {
            CreateMap<County, CountyDTO>()
                .ReverseMap();
        }

    }
}
