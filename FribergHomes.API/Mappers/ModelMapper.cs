using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using FribergHomes.API.Data.Interfaces;

namespace FribergHomes.API.Mappers
{
    public static class ModelMapper
    {
        /* Makes RealtorDTO to a Realtor
         * @ Author: Rebecka 2024-04-24
         */
        public static Realtor DtoToRealtor(RealtorDTO realtorDto) 
        {
            if (realtorDto == null)
                return null;

            string[] names = realtorDto.FullName.Split(' ',2);
            string firstName = names[0];
            string lastName = names[1];

            var realtor = new Realtor()
            {
                Id = realtorDto.Id,
                FirstName = firstName,
                LastName = lastName,
                Email = realtorDto.Email,
                PhoneNumber = realtorDto.PhoneNumber,
                Picture = realtorDto.Picture,
                Agency = new Agency() { 
                    Name = realtorDto.Agency,
                    Logo = "dublett",
                    Presentation = "dublett"
                }
                // TODO: se över
            };
            realtor.SalesObjects = ModelMapper.DtoListToSalesObjectList(realtorDto.SalesObjects, realtor);

            return realtor;
        }

        /* Makes AgencyDTO to a Agency
         * @ Author: Rebecka 2024-04-24
         */
        public static Agency DtoToAgency(AgencyDTO agencyDto)
        {
            if (agencyDto == null)
            {
                return null;
            }

            Agency agency = new Agency
            {
                Id = agencyDto.Id,
                Name = agencyDto.Name,
                Presentation = agencyDto.Presentation,
                Logo = agencyDto.Logo,
            };
            return agency;
        }

        /* Makes SalesObjectDTO to a SalesObject
         * @ Author: Rebecka 2024-04-24
         */
        public static SalesObject DtoToSalesObject(SalesObjectDTO salesObjectDto)
        {
            SalesObject salesObject = new SalesObject()
            {
                Id = salesObjectDto.Id,
                CreationDate = salesObjectDto.CreationDate,
                CreatorName = salesObjectDto.CreatorName,
                ChangeDate = salesObjectDto.ChangeDate,
                ChangeName = salesObjectDto.ChangeName,
                Adress = salesObjectDto.Adress,
                Rooms = salesObjectDto.Rooms,
                LivingArea = salesObjectDto.LivingArea,
                AncillaryArea = salesObjectDto.AncillaryArea,
                PlotArea = salesObjectDto.PlotArea,
                YearlyCost = salesObjectDto.YearlyCost,
                MonthlyFee = salesObjectDto.MonthlyFee,
                Level = salesObjectDto.Level,
                Lift = salesObjectDto.Lift,
                ListingPrice = salesObjectDto.ListingPrice,
                CurrentPrice = salesObjectDto.CurrentPrice,
                ObjectDescription = salesObjectDto.ObjectDescription,
                BuildYear = salesObjectDto.BuildYear,
                ImageLinks = salesObjectDto.ImageLinks,
                ViewingDates = salesObjectDto.ViewingDates,
                Category = salesObjectDto.Category,
                County = new County()
                {
                    Name = salesObjectDto.CountyName
                }
            };

            // TODO: Realtor...?


            return salesObject;
        }

        /* Makes SalesObjectDTO-List to a SalesObject-List
         * @ Author: Rebecka 2024-04-25
         */
        public static List<SalesObject> DtoListToSalesObjectList(List<SalesObjectDTO> salesObjectDtos, Realtor realtor)
        {
            if (salesObjectDtos == null)
                return null;

            List<SalesObject> salesObjects = new List<SalesObject>();

            foreach (var dto in salesObjectDtos)
            {

                SalesObject salesObject = new SalesObject()
                {
                    Id = dto.Id,
                    CreationDate = dto.CreationDate,
                    CreatorName = dto.CreatorName,
                    ChangeDate = dto.ChangeDate,
                    ChangeName = dto.ChangeName,
                    Adress = dto.Adress,
                    Rooms = dto.Rooms,
                    LivingArea = dto.LivingArea,
                    AncillaryArea = dto.AncillaryArea,
                    PlotArea = dto.PlotArea,
                    YearlyCost = dto.YearlyCost,
                    MonthlyFee = dto.MonthlyFee,
                    Level = dto.Level,
                    Lift = dto.Lift,
                    ListingPrice = dto.ListingPrice,
                    CurrentPrice = dto.CurrentPrice,
                    ObjectDescription = dto.ObjectDescription,
                    BuildYear = dto.BuildYear,
                    ImageLinks = dto.ImageLinks,
                    ViewingDates = dto.ViewingDates,
                    Category = dto.Category,
                    County = new County()
                    {
                        Name = dto.CountyName
                    },
                    Realtor = realtor
                };

                salesObjects.Add(salesObject);

            }
            return salesObjects;
        }


        /* Makes CountyDTO to a County
         * @ Author: Rebecka 2024-04-25
         */
        public static County DtoToCounty(CountyDTO countyDto)
        {
            if (countyDto == null)
                return null;

            County county = new County
            {
                Id = countyDto.Id,
                Name = countyDto.Name,
            };

            return county;
        }

        /* Makes CountyDTO-List to a County-List
         * @ Author: Rebecka 2024-04-25
         */
        public static List<County> MapCountyListToDtoList(List<CountyDTO> countiesDto)
        {
            if (countiesDto == null)
                return null;

            List<County> counties = new List<County>();

            foreach (var countyDto in countiesDto)
            {
                County county = new County
                {
                    Id = countyDto.Id,
                    Name = countyDto.Name,
                };
                counties.Add(county);
            }
            return counties;
        }

    }
}
