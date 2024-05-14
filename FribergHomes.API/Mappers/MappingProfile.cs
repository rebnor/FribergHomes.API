using AutoMapper;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    // Author: Tobias 2024-05-03

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // SalesObject -> SalesObjectDTO
            CreateMap<SalesObject, SalesObjectDTO>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                //.ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.CreatorName))
                //.ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.ChangeDate))
                //.ForMember(dest => dest.ChangeName, opt => opt.MapFrom(src => src.ChangeName))
                //.ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                //.ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                //.ForMember(dest => dest.LivingArea, opt => opt.MapFrom(src => src.LivingArea))
                //.ForMember(dest => dest.AncillaryArea, opt => opt.MapFrom(src => src.AncillaryArea))
                //.ForMember(dest => dest.PlotArea, opt => opt.MapFrom(src => src.PlotArea))
                //.ForMember(dest => dest.YearlyCost, opt => opt.MapFrom(src => src.YearlyCost))
                //.ForMember(dest => dest.MonthlyFee, opt => opt.MapFrom(src => src.MonthlyFee))
                //.ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                //.ForMember(dest => dest.Lift, opt => opt.MapFrom(src => src.Lift))
                //.ForMember(dest => dest.ListingPrice, opt => opt.MapFrom(src => src.ListingPrice))
                //.ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.CurrentPrice))
                //.ForMember(dest => dest.ObjectDescription, opt => opt.MapFrom(src => src.ObjectDescription))
                //.ForMember(dest => dest.BuildYear, opt => opt.MapFrom(src => src.BuildYear))
                //.ForMember(dest => dest.ImageLinks, opt => opt.MapFrom(src => src.ImageLinks))
                //.ForMember(dest => dest.ViewingDates, opt => opt.MapFrom(src => src.ViewingDates))
                .ForMember(dest => dest.RealtorId, opt => opt.MapFrom(src => src.Realtor!.Id))
                .ForMember(dest => dest.RealtorName, opt => opt.MapFrom(src => $"{src.Realtor!.FirstName} {src.Realtor!.LastName}"))
                //.ForMember(dest => dest.RealtorEmail, opt => opt.MapFrom(src => src.Realtor!.Email))
                .ForMember(dest => dest.RealtorPhone, opt => opt.MapFrom(src => src.Realtor!.PhoneNumber))
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Realtor!.Agency.Id))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Realtor!.Agency.Name))
                .ForMember(dest => dest.AgencyLogoUrl, opt => opt.MapFrom(src => src.Realtor!.Agency.Logo));
                //.ForMember(dest => dest.CountyId, opt => opt.MapFrom(src => src.County!.Id))
                //.ForMember(dest => dest.CountyName, opt => opt.MapFrom(src => src.County!.Name))
                //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category!.Id))
                //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
                //.ForMember(dest => dest.CategoryLogoUrl, opt => opt.MapFrom(src => src.Category!.IconUrl));

            // SalesObjectDTO -> SalesObject
            CreateMap<SalesObjectDTO, SalesObject>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                //.ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.CreatorName))
                //.ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.ChangeDate))
                //.ForMember(dest => dest.ChangeName, opt => opt.MapFrom(src => src.ChangeName))
                //.ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                //.ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                //.ForMember(dest => dest.LivingArea, opt => opt.MapFrom(src => src.LivingArea))
                //.ForMember(dest => dest.AncillaryArea, opt => opt.MapFrom(src => src.AncillaryArea))
                //.ForMember(dest => dest.PlotArea, opt => opt.MapFrom(src => src.PlotArea))
                //.ForMember(dest => dest.YearlyCost, opt => opt.MapFrom(src => src.YearlyCost))
                //.ForMember(dest => dest.MonthlyFee, opt => opt.MapFrom(src => src.MonthlyFee))
                //.ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                //.ForMember(dest => dest.Lift, opt => opt.MapFrom(src => src.Lift))
                //.ForMember(dest => dest.ListingPrice, opt => opt.MapFrom(src => src.ListingPrice))
                //.ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.CurrentPrice))
                //.ForMember(dest => dest.ObjectDescription, opt => opt.MapFrom(src => src.ObjectDescription))
                //.ForMember(dest => dest.BuildYear, opt => opt.MapFrom(src => src.BuildYear))
                //.ForMember(dest => dest.ImageLinks, opt => opt.MapFrom(src => src.ImageLinks))
                //.ForMember(dest => dest.ViewingDates, opt => opt.MapFrom(src => src.ViewingDates))
                .ForMember(dest => dest.Realtor, opt => opt.MapFrom<SORealtorResolver>())
                .ForMember(dest => dest.County, opt => opt.MapFrom<SOCountyResolver>())
                .ForMember(dest => dest.Category, opt => opt.MapFrom<SOCategoryResolver>());

            // Realtor -> RealtorDTO
            CreateMap<Realtor, RealtorDTO>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                //.ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency!.Id))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency!.Name))
                .ForMember(dest => dest.AgencyLogo, opt => opt.MapFrom(src => src.Agency!.Logo));

            // RealtorDTO -> Realtor
            CreateMap<RealtorDTO, Realtor>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom<RealtorFullNameResolver>())
                .ForMember(dest => dest.LastName, opt => opt.MapFrom<RealtorFullNameResolver>())
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                //.ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))
                .ForMember(dest => dest.Agency, opt => opt.MapFrom<RealtorAgencyResolver>());

            // RegisterRealtorDTO -> Realtor    //TODO: Implementera om AuthController ska använda AutoMapper. / Tobias
            //CreateMap<RegisterRealtorDTO, Realtor>()
            //    .ForMember(dest => dest.Agency, opt => opt.MapFrom<RealtorAgencyResolver>());

            // Agency <-> AgencyDTO
            CreateMap<Agency, AgencyDTO>()
                .ReverseMap();

            // County <-> CountyDTO
            CreateMap<County, CountyDTO>()
                .ReverseMap();

            // Category <-> CategoryDTO
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();

        }

    }
}
