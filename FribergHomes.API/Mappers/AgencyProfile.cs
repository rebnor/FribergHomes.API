using AutoMapper;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class AgencyProfile : Profile
    {
        public AgencyProfile() 
        {
            CreateMap<Agency, AgencyDTO>()
                .ReverseMap();
        }

    }
}
