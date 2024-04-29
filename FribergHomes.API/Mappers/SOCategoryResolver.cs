using AutoMapper;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class SOCategoryResolver : IValueResolver<SalesObjectDTO, SalesObject, Category?>
    {
        private readonly ICategory _categoryRepository;

        public SOCategoryResolver(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category? Resolve(SalesObjectDTO src, SalesObject dest, Category? destMember, ResolutionContext context)
        {
            var categoryId = src.CategoryId;
            var category = _categoryRepository.GetCategoryByIdAsync(categoryId).Result;

            return category;
        }
    }
}
