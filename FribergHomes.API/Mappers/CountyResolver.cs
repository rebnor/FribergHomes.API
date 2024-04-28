using AutoMapper;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class CountyResolver : IValueResolver<SalesObjectDTO, SalesObject, County?>
    {
        private readonly ICounty _countyRepository;

        public CountyResolver(ICounty countyRepository)
        {
            _countyRepository = countyRepository;
        }

        public County? Resolve(SalesObjectDTO src, SalesObject dest, County? destMember, ResolutionContext context)
        {
            var countyId = src.CountyId;
            var county = _countyRepository.GetCountyByIdAsync(countyId).Result;

            return county;
        }

    }
}
