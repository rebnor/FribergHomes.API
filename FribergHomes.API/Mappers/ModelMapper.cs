using FribergHomes.API.DTOs;
using FribergHomes.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using FribergHomes.API.Data.Interfaces;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

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

            string[] names = realtorDto.FullName.Split(' ', 2);
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
                Agency = new Agency()
                { // Se över om detta går
                    Name = realtorDto.Agency,
                    Logo = "dublett",
                    Presentation = "dublett"
                }
                // SalesObject
            };
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
         * Not all done, need to see how Realtor will be handled
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
                County = salesObjectDto.County
            };

            // TODO: Realtor...?

            return salesObject;
        }

        // Author: Tobias 2024-04-25
        /// <summary>
        /// Creates a new Category object based on the provided CategoryDTO object.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns>A Category object</returns>
        public static Category ToCategory(CategoryDTO categoryDto)
        {
            Category category = new()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                IconUrl = categoryDto.IconUrl
            };

            return category;
        }

        // Author: Tobias 2024-04-25
        /// <summary>
        /// Creates a new List&lt;Category&gt; based on the provided List&lt;CategoryDTO&gt;.
        /// </summary>
        /// <param name="categoryDtos"></param>
        /// <returns>A List&lt;Category&gt;</returns>
        public static List<Category> ToListCategory(List<CategoryDTO> categoryDtos)
        {
            List<Category> categories = new();

            foreach (var categoryDto in categoryDtos)
            {
                Category category = new()
                {
                    Id = categoryDto.Id,
                    Name = categoryDto.Name,
                    IconUrl = categoryDto.IconUrl
                };

                categories.Add(category);
            }

            return categories;
        }

    }
}
