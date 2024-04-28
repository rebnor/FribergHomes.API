using AutoMapper;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class RealtorResolver : IValueResolver<SalesObjectDTO, SalesObject, Realtor?>
    {
        private readonly IRealtor _realtorRepository;

        public RealtorResolver(IRealtor realtorRepository)
        {
            _realtorRepository = realtorRepository;
        }

        public Realtor? Resolve(SalesObjectDTO src,  SalesObject dest, Realtor? destMember, ResolutionContext context)
        {
            var realtorId = src.RealtorId;
            var realtor = _realtorRepository.GetRealtorByIdAsync(realtorId).Result;

            return realtor;
        }
    }
}
