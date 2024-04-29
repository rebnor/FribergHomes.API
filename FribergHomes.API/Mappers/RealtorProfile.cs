using AutoMapper;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class RealtorProfile : Profile
    {
        public RealtorProfile() 
        {
            CreateMap<Realtor, RealtorDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency!.Id))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency!.Name))
                .ForMember(dest => dest.AgencyLogo, opt => opt.MapFrom(src => src.Agency!.Logo));

            CreateMap<RealtorDTO, Realtor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom<RealtorFullNameResolver>())
                .ForMember(dest => dest.LastName, opt => opt.MapFrom<RealtorFullNameResolver>())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))
                .ForMember(dest => dest.Agency, opt => opt.MapFrom<RealtorAgencyResolver>());
        }

    }
}
