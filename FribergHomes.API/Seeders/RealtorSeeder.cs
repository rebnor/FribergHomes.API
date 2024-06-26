﻿using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Data.Repositories;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Seeders
{
    public class RealtorSeeder
    {      
        // Author: Sanna 2024-04-17  
        // Update: Added some realtors / Reb 2024-05-03
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
                       Picture = "https://img.freepik.com/free-photo/front-view-smiley-business-man_23-2148479583.jpg?t=st=1714727448~exp=1714731048~hmac=2cbf6ab94556f4dec5f8b58b27adee549ecadd45b619b364853d32fcd11f4a37&w=1380",
                       Agency = agencies[3]
                       //SalesObjects = null
                   },
                   new Realtor
                   {
                       FirstName = "Fia",
                       LastName = "Knuff",
                       Email = "fia@företagsmail.se",
                       PhoneNumber = "070 555 55 55",
                       Picture = "https://img.freepik.com/free-photo/portrait-woman-therapist_23-2148759115.jpg?t=st=1714727423~exp=1714731023~hmac=07b39e1b82b85df68c215bd3d35f5ce831485a70faeb3a9512137f9b0655b848&w=1380",
                       Agency = agencies[4]
                       //SalesObjects = null
                   },
                   new Realtor
                   {
                       FirstName = "Sara",
                       LastName = "Andersson",
                       Email = "sara@företagsmail.se",
                       PhoneNumber = "070 666 66 66",
                       Picture = "https://images.pexels.com/photos/415829/pexels-photo-415829.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                       Agency = agencies[0]
                       //SalesObjects = null
                   },
                   new Realtor
                   {
                       FirstName = "Anna",
                       LastName = "Larsson",
                       Email = "anna@företagsmail.se",
                       PhoneNumber = "070 777 77 77",
                       Picture = "https://img.freepik.com/free-photo/young-businesswoman-portrait-office_1262-1506.jpg?t=st=1714727293~exp=1714730893~hmac=6b04ab5c2ad4f39460726566822d0bcc6a0dce3ca6a88f470659dab5c9d5ca8d&w=1800",
                       Agency = agencies[1]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Lena",
                       LastName = "Karlsson",
                       Email = "lena@företagsmail.se",
                       PhoneNumber = "070 888 88 88",
                       Picture = "https://img.freepik.com/free-photo/front-view-business-woman-suit_23-2148603018.jpg?t=st=1714727339~exp=1714730939~hmac=c205d49eb1596d8b9042cfc0571ac96a5cc98507723eb894b21861817011966c&w=1380",
                       Agency = agencies[2]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Karin",
                       LastName = "Olsson",
                       Email = "karin@företagsmail.se",
                       PhoneNumber = "070 999 99 99",
                       Picture = "https://img.freepik.com/free-photo/close-up-successful-woman-with-blue-shirt_1098-3627.jpg?t=st=1714727349~exp=1714730949~hmac=a073704acc186e52015486839d1e04ac1233720206ea2d18c492e8eb7152fc0f&w=1800",
                       Agency = agencies[3]
                       //SalesObjects = null
                   }
                     ,
                   new Realtor
                   {
                       FirstName = "Josefine",
                       LastName = "Hammar",
                       Email = "josefine@företagsmail.se",
                       PhoneNumber = "070 101 10 10",
                       Picture = "https://img.freepik.com/free-photo/stylish-businesswoman-with-glasses_23-2147989567.jpg?t=st=1714727364~exp=1714730964~hmac=64ccfb6a423ee836db2631212e259e12bf3b2a436d1ca74ec53ab21567c90e4e&w=1380",
                       Agency = agencies[4]
                       //SalesObjects = null
                   }
                        ,
                   new Realtor
                   {
                       FirstName = "Mikaela",
                       LastName = "Forsström",
                       Email = "mikaela@företagsmail.se",
                       PhoneNumber = "070 101 20 20",
                       Picture = "https://img.freepik.com/free-photo/cheerful-young-businesswoman-smiling-camera_74855-4022.jpg?t=st=1714727383~exp=1714730983~hmac=08f429dab8c5d2b43839c0c90b510507b5230db6bde630ca80bf9683ad9d9d8f&w=1800",
                       Agency = agencies[0]
                       //SalesObjects = null
                   }
                    ,
                   new Realtor
                   {
                       FirstName = "Erik",
                       LastName = "Svensson",
                       Email = "erik@företagsmail.se",
                       PhoneNumber = "070 101 30 30",
                       Picture = "https://img.freepik.com/free-photo/happy-young-businessman-walking-near-business-center_171337-19784.jpg?t=st=1714727322~exp=1714730922~hmac=29de912b86772be13636ff314dc84c68418ec0e579e06bcba03fd61013e946c9&w=1800",
                       Agency = agencies[1]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Thomas",
                       LastName = "Eriksson",
                       Email = "thomas@företagsmail.se",
                       PhoneNumber = "070 101 40 40",
                       Picture = "https://img.freepik.com/free-photo/portrait-smiley-business-man_23-2148514859.jpg?t=st=1714727336~exp=1714730936~hmac=22030532f6bd4aa05a6cf91bff731ea53c11e47a22d14c6b45e54786be28bb9a&w=1800",
                       Agency = agencies[2]
                       //SalesObjects = null
                   }
                     ,
                   new Realtor
                   {
                       FirstName = "Fredrik",
                       LastName = "Johnsson",
                       Email = "fredrik@företagsmail.se",
                       PhoneNumber = "070 101 50 50",
                       Picture = "https://img.freepik.com/free-photo/portrait-optimistic-businessman-formalwear_1262-3600.jpg?t=st=1714727343~exp=1714730943~hmac=2d74b7487ee85b284243fbfec5d402079a32857820a9772e7f84efe419b7bfdc&w=1800",
                       Agency = agencies[3]
                       //SalesObjects = null
                   }
                     ,
                   new Realtor
                   {
                       FirstName = "Marcus",
                       LastName = "Holmström",
                       Email = "marcusk@företagsmail.se",
                       PhoneNumber = "070 101 60 60",
                       Picture = "https://img.freepik.com/free-photo/happy-successful-businessman-posing-outside_74855-2004.jpg?t=st=1714727351~exp=1714730951~hmac=4276b8687aa143ba6748d56a452e0b10807a5c4c795d7414e9453edc99089694&w=1800",
                       Agency = agencies[4]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Torsten",
                       LastName = "Ekström",
                       Email = "torsten@företagsmail.se",
                       PhoneNumber = "070 101 70 70",
                       Picture = "https://img.freepik.com/free-photo/close-up-young-businessman_23-2149153813.jpg?t=st=1714727356~exp=1714730956~hmac=91998c2b8fc41feb5e2552b0651d716a85617f84d73e546f605c53b497a9a148&w=1800",
                       Agency = agencies[0]
                       //SalesObjects = null
                   }
                     ,
                   new Realtor
                   {
                       FirstName = "Allan",
                       LastName = "Gustavsson",
                       Email = "allan@företagsmail.se",
                       PhoneNumber = "070 101 80 80",
                       Picture = "https://img.freepik.com/free-photo/smiley-front-view-business-man_23-2148763836.jpg?t=st=1714727373~exp=1714730973~hmac=ba5441cf51105376dcf787bb568ed98ae949bd3885b160b117fb19901e2a66b1&w=1800",
                       Agency = agencies[1]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Christian",
                       LastName = "Axelsson",
                       Email = "christian@företagsmail.se",
                       PhoneNumber = "070 101 90 90",
                       Picture = "https://img.freepik.com/free-photo/smiling-man-suit-looking-camera_23-2148112202.jpg?t=st=1714727379~exp=1714730979~hmac=f5559267c8635a1d23ad06c082f0df421cf0b818390d0dd2c93814f428e81b10&w=1800",
                       Agency = agencies[2]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Susanna",
                       LastName = "Söderström",
                       Email = "susanna@företagsmail.se",
                       PhoneNumber = "070 202 10 10",
                       Picture = "https://img.freepik.com/free-photo/closeup-young-female-professional-making-eye-contact-against-colored-background_662251-651.jpg?t=st=1714727408~exp=1714731008~hmac=79eb6ee9269a9183269c661bf74935c844082b43834ea4619feb6f52caca0f65&w=1060",
                       Agency = agencies[3]
                       //SalesObjects = null
                   }
                   ,
                   new Realtor
                   {
                       FirstName = "Joakim",
                       LastName = "Holmström",
                       Email = "joakim@företagsmail.se",
                       PhoneNumber = "070 202 20 20",
                       Picture = "https://img.freepik.com/free-photo/young-businessman-happy-expression_1194-1641.jpg?t=st=1714727393~exp=1714730993~hmac=d4faa47269465b8b6f2f27765c41e09c78eb9ed54558b076174331c4df6f2a2a&w=1800",
                       Agency = agencies[4]
                       //SalesObjects = null
                   }
                  );
                await appDbCtx.SaveChangesAsync();
            }
        }
    }
}
