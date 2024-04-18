using FribergHomes.API.Data;
using FribergHomes.API.Models;
using NuGet.Packaging.Rules;

namespace FribergHomes.API.Seeders
{
    public class CategorySeeder
    {
        //Author: Sanna 2024-04-18
        public async Task SeedCategories(ApplicationDBContext appDbCtx)
        {
            if(!appDbCtx.Categories.Any())
            {
                await appDbCtx.AddRangeAsync(
                new Category
                {
                    Name = "Apartment",
                    IconUrl = "https://fonts.google.com/icons?selected=Material%20Symbols%20Outlined%3Aapartment%3AFILL%400%3Bwght%40400%3BGRAD%400%3Bopsz%4048"
                },
                new Category
                {
                    Name = "House", 
                    IconUrl = "https://fonts.google.com/icons?selected=Material%20Symbols%20Outlined%3Ahouse%3AFILL%400%3Bwght%40400%3BGRAD%400%3Bopsz%4048"
                },
                new Category
                {
                    Name = "TownHouse",
                    IconUrl = "https://fonts.google.com/icons?selected=Material%20Symbols%20Outlined%3Aholiday_village%3AFILL%400%3Bwght%40400%3BGRAD%400%3Bopsz%4048"
                },
                new Category
                {
                    Name = "VacationHome",
                    IconUrl = "https://fonts.google.com/icons?selected=Material%20Symbols%20Outlined%3Aother_houses%3AFILL%400%3Bwght%40400%3BGRAD%400%3Bopsz%4048"
                });
                await appDbCtx.SaveChangesAsync();
            }
        }
    }
}
