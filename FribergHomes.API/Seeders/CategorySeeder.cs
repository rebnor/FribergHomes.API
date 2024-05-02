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
                    IconUrl = "<span class=\"material-symbols-outlined\">\r\napartment\r\n</span>"
                },
                new Category
                {
                    Name = "House", 
                    IconUrl = "<span class=\"material-symbols-outlined\">\r\nhouse\r\n</span>"
                },
                new Category
                {
                    Name = "TownHouse",
                    IconUrl = "<span class=\"material-symbols-outlined\">\r\nholiday_village\r\n</span>"
                },
                new Category
                {
                    Name = "VacationHome",
                    IconUrl = "<span class=\"material-symbols-outlined\">\r\nother_houses\r\n</span>"
                });
                await appDbCtx.SaveChangesAsync();
            }
        }
    }
}
