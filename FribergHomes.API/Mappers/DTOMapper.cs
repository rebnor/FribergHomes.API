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
                SalesObjects = ToListSalesObjectDTO(realtor.SalesObjects)
            };

            return realtorDto;
        }
        /* DTO-Mapper for Realtor List
         * @ Author: Rebecka 2024-04-24        
         */
        public static List<RealtorDTO> MapRealtorListToDto(List<Realtor> realtors)
        {
            if (realtors == null)
                return null;

            List<RealtorDTO> realtorsDtos = new List<RealtorDTO>();

            foreach (var realtor in realtors)
            {
                RealtorDTO realtorDto = new RealtorDTO
                {
                    Id = realtor.Id,
                    FullName = $"{realtor.FirstName} {realtor.LastName}",
                    Email = realtor.Email,
                    PhoneNumber = realtor.PhoneNumber,
                    Picture = realtor.Picture,
                    Agency = realtor.Agency.Name,
                    AgencyLogo = realtor.Agency.Logo,
                    SalesObjects = ToListSalesObjectDTO(realtor.SalesObjects)
                };
                realtorsDtos.Add(realtorDto);
            }
            return realtorsDtos;
        }


        // Mapping function for SalesObject -> SalesObjectDTO    /Tobias 2024-04-23
        /// <summary>
        /// Creates a new SalesObjectDTO based on an existing SalesObject instance.
        /// </summary>
        /// <param name="salesObject">SalesObject entity</param>
        /// <returns>An object of type SalesObjectDTO</returns>
        /// @ Update: Switched from County-Object to CountyName / Reb 2024-05-25
        public static SalesObjectDTO ToSalesObjectDTO(SalesObject salesObject)
        {
            SalesObjectDTO salesObjectDTO = new()
            {
                Id = salesObject.Id,
                CreationDate = salesObject.CreationDate,
                CreatorName = salesObject.CreatorName,
                Adress = salesObject.Adress,
                Rooms = salesObject.Rooms,
                LivingArea = salesObject.LivingArea,
                AncillaryArea = salesObject.AncillaryArea,
                PlotArea = salesObject.PlotArea,
                YearlyCost = salesObject.YearlyCost,
                MonthlyFee = salesObject.MonthlyFee,
                Level = salesObject.Level,
                Lift = salesObject.Lift,
                ListingPrice = salesObject.ListingPrice,
                CurrentPrice = salesObject.CurrentPrice,
                ObjectDescription = salesObject.ObjectDescription,
                BuildYear = salesObject.BuildYear,
                ImageLinks = salesObject.ImageLinks,
                ViewingDates = salesObject.ViewingDates,
                RealtorId = salesObject.Realtor.Id,
                RealtorName = $"{salesObject.Realtor.FirstName} {salesObject.Realtor.LastName}",
                RealtorEmail = salesObject.Realtor.Email,
                RealtorPhone = salesObject.Realtor.PhoneNumber,
                AgencyName = salesObject.Realtor.Agency.Name,
                AgencyLogoUrl = salesObject.Realtor.Agency.Logo,
                CountyName = salesObject.County.Name,
                Category = salesObject.Category
            };

            return salesObjectDTO;
        }

        // Mapping function for List<SalesObject> -> List<SalesObjectDTO>    /Tobias 2024-04-23
        /// <summary>
        /// Creates a new List of SalesObjectDTOs based on an existing List of SalesObjects.
        /// </summary>
        /// <param name="salesObjects">List of SalesObjects</param>
        /// <returns>A List of SalesObjectDTOs</returns>
        /// @ Update: Switched from County-Object to CountyName / Reb 2024-05-25
        public static List<SalesObjectDTO> ToListSalesObjectDTO(List<SalesObject> salesObjects)
        {
            List<SalesObjectDTO> salesObjectDTOs = new();

            if (salesObjects != null) // added reb 
            {

                foreach (var salesObject in salesObjects)
                {
                    SalesObjectDTO salesObjectDTO = new()
                    {
                        Id = salesObject.Id,
                        CreationDate = salesObject.CreationDate,
                        CreatorName = salesObject.CreatorName,
                        Adress = salesObject.Adress,
                        Rooms = salesObject.Rooms,
                        LivingArea = salesObject.LivingArea,
                        AncillaryArea = salesObject.AncillaryArea,
                        PlotArea = salesObject.PlotArea,
                        YearlyCost = salesObject.YearlyCost,
                        MonthlyFee = salesObject.MonthlyFee,
                        Level = salesObject.Level,
                        Lift = salesObject.Lift,
                        ListingPrice = salesObject.ListingPrice,
                        CurrentPrice = salesObject.CurrentPrice,
                        ObjectDescription = salesObject.ObjectDescription,
                        BuildYear = salesObject.BuildYear,
                        ImageLinks = salesObject.ImageLinks,
                        ViewingDates = salesObject.ViewingDates,
                        RealtorId = salesObject.Realtor.Id,
                        RealtorName = $"{salesObject.Realtor.FirstName} {salesObject.Realtor.LastName}",
                        RealtorEmail = salesObject.Realtor.Email,
                        RealtorPhone = salesObject.Realtor.PhoneNumber,
                        AgencyName = salesObject.Realtor.Agency.Name,
                        AgencyLogoUrl = salesObject.Realtor.Agency.Logo,
                        CountyName = salesObject.County.Name,
                        Category = salesObject.Category
                    };

                    salesObjectDTOs.Add(salesObjectDTO);
                }
                return salesObjectDTOs;
            }
            return null;
        }

        //DTO mapper for Agency
        //Author: Sanna 2024-04-23
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

        public static List<AgencyDTO> MapAgenciesToDtos(List<Agency> agencies) 
        {
           var agencyDTOs = new List<AgencyDTO>();

            foreach (Agency agency in agencies) 
            {
                var agencyDTO = new AgencyDTO
                {
                    Id = agency.Id,
                    Name = agency.Name,
                    Presentation = agency.Presentation,
                    Logo = agency.Logo,
                };
                agencyDTOs.Add(agencyDTO);
            }
                return agencyDTOs;               
        }


        /* DTO-Mapper for County
         * @ Author: Rebecka 2024-04-25        
         */
        public static CountyDTO MapCountyToDto(County county)
        {
            if (county == null)
                return null;

            CountyDTO countyDto = new CountyDTO
            {
                Id = county.Id,
                Name = county.Name,
            };

            return countyDto;
        }
        /* DTO-Mapper for County-List
         * @ Author: Rebecka 2024-04-25        
         */
        public static List<CountyDTO> MapCountyListToDtoList(List<County> counties)
        {
            if (counties == null)
                return null;

            List<CountyDTO> countiesDto = new List<CountyDTO>();

            foreach (var county in counties)
            {
                CountyDTO countyDto = new CountyDTO
                {
                    Id = county.Id,
                    Name = county.Name,
                };
                countiesDto.Add(countyDto);
            }
            
            return countiesDto;
        }



    }
}
