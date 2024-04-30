using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Data.Repositories;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Seeders
{
    public class RealtorSeeder
    {      
        // Author: Sanna 2024-04-17  

        public async Task SeedRealtors(ApplicationDBContext appDbCtx)
        {
            if (!appDbCtx.Realtors.Any())
            {
                var agencies = await appDbCtx.Agencies.OrderBy(a => a.Id).ToListAsync();
                await appDbCtx.AddRangeAsync(
                  new Realtor
                  {
                      FirstName = "Sanna",
                      LastName = "Nyberg",
                      Email = "sanna@företagsmail.se",
                      PhoneNumber = "070 111 11 11",
                      Picture = "https://media.istockphoto.com/id/536974271/sv/foto/dipsy-the-green-alien-teletubby-character.jpg?s=612x612&w=0&k=20&c=NnZMOULys48guc4nK0sfJ_mM4wzctrVov5ZzNWMenLU=",
                      Agency = agencies[0]
                      //SalesObjects = null
                  }
                  , new Realtor
                  {
                      FirstName = "Rebecka",
                      LastName = "Nordqvist",
                      Email = "rebecka@företagsmail.se",
                      PhoneNumber = "070 222 22 22",
                      Picture = "https://imusic.b-cdn.net/images/item/original/074/5029736061074.jpg?character-teletubbies-8-inch-talking-soft-po-merch&class=scaled&v=1616863454",
                      Agency = agencies[1]
                      //SalesObjects = null
                  }
                  ,
                   new Realtor
                   {
                       FirstName = "Tobias",
                       LastName = "Ledin",
                       Email = "tobias@företagsmail.se",
                       PhoneNumber = "070 333 33 33",
                       Picture = "https://m.media-amazon.com/images/I/81QOLXkVnzL._AC_UF1000,1000_QL80_.jpg",
                       Agency = agencies[2]
                       //SalesObjects = null
                   },
                   new Realtor
                   {
                       FirstName = "Harry",
                       LastName = "Boy",
                       Email = "harry@företagsmail.se",
                       PhoneNumber = "070 444 44 44",
                       Picture = "https://www.scoliosis-rehabilitation.com/mymedia/2016/06/no-face.png",
                       Agency = agencies[3]
                       //SalesObjects = null
                   },
                   new Realtor
                   {
                       FirstName = "Fia",
                       LastName = "Knuff",
                       Email = "fia@företagsmail.se",
                       PhoneNumber = "070 555 55 55",
                       Picture = "https://www.scoliosis-rehabilitation.com/mymedia/2016/06/no-face.png",
                       Agency = agencies[4]
                       //SalesObjects = null
                   }
                  );
                await appDbCtx.SaveChangesAsync();
            }
        }
    }
}
