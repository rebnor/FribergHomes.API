using AutoMapper;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class RealtorAgencyResolver : IValueResolver<RealtorDTO, Realtor, Agency>
    {
        private readonly IAgency _agencyRepository;

        public RealtorAgencyResolver(IAgency agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        public Agency Resolve(RealtorDTO src, Realtor dest, Agency destMember, ResolutionContext context)
        {
            var agencyId = src.AgencyId;
            var agency = _agencyRepository.GetAgencyByIdAsync(agencyId).Result;

            return agency;
        }
    }
}