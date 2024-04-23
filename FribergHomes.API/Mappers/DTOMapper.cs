using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using System;

namespace FribergHomes.API.Mappers
{
    public class DTOMapper
    {
        /* DTO-Mapper for Realtor
         * @ Author: Rebecka 2024-04-23        
         */
        public static RealtorDTO MapRealtorToDto(Realtor realtor)
        {
            if (realtor == null)
                return null;

            RealtorDTO realtorDto = new RealtorDTO
            {
                Id = realtor.Id,
                FullName = $"{realtor.FirstName} {realtor.LastName}",
                Email = realtor.Email,
                PhoneNumber = realtor.PhoneNumber,
                Picture = realtor.Picture,
                Agency = realtor.Agency.Name,
                AgencyLogo = realtor.Agency.Logo,
                // SalesObjects = MapSalesObjectsToDto(realtor.SalesObjects) // Se vad metoden heter när den är färdig
            };

            return realtorDto;
        }

        //DTO mapper for Agency
        //Author: Sanna 
        public static AgencyDTO MapAgencyToDto(Agency agency) 
        {
            if (agency == null)
            {
                return null; 
            }

            AgencyDTO agencyDTO = new AgencyDTO
            {
                Id = agency.Id,
                Name = agency.Name,
                Presentation = agency.Presentation,
                Logo = agency.Logo,
            };
            return agencyDTO;
        }

    }
}
