using AutoMapper;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        { 
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
 
    }
}
