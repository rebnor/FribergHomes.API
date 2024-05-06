using FribergHomes.API.Data;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Net.WebRequestMethods;

namespace FribergHomes.API.Seeders
{
    /* Seeder class that checks if DB table SalesObjects contains any entries.
     * If not, creates new SalesObject objects and populates the DB table.
     * Author: Tobias 2024-04-19
     * Update: Added more pictures to different salesobject / Reb 2024-05-03
     */
    public class SalesObjectSeeder
    {
        private readonly ApplicationDBContext _appDbContext;

        public SalesObjectSeeder(ApplicationDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // <parameter> Amount of SalesObjects to generate.
        public async Task SeedSalesObjects(int objectAmount)
        {
            //        string[] imgHouse =
            //{
            //            "https://www.a-hus.se/storage/4CBEE1AB262DB926EFE91C41ACBF0CB8D09D3B6B01D420DDF5018865C8AA0E48/579cece4bb5e47da88993b28b4484aae/jpg/media/512f835bc0194897ae17b63334635b67/Bj%C3%B6rk%C3%B6%20Klassisk1920x1920.jpg"
            //            };
            //        string[] imgApartment =
            //            {
            //            "https://meltwater-apps-production.s3.eu-west-1.amazonaws.com/uploads/images/582addd000e4eb2b7dd9df7f/blobid4_1614761878277.png"
            //            };
            //        string[] imgTownhouse =
            //            {
            //            "https://www.bonava.se/siteassets/projekt/altadalen/radhus-pionen-1180x500.jpg"
            //            };
            //        string[] imgVacationhome =
            //            {
            //            "https://www.stugaonline.se/thumb/586/1280x0/fellin2.jpg"
            //            };


            #region pictures

            List<string[]> picStringsHouse = new List<string[]>();

            string[] imgHouse =
                {
                "https://www.a-hus.se/storage/4CBEE1AB262DB926EFE91C41ACBF0CB8D09D3B6B01D420DDF5018865C8AA0E48/579cece4bb5e47da88993b28b4484aae/jpg/media/512f835bc0194897ae17b63334635b67/Bj%C3%B6rk%C3%B6%20Klassisk1920x1920.jpg",
                "https://cdn.pixabay.com/photo/2016/11/18/17/20/living-room-1835923_1280.jpg"
                };
            string[] imgGreyHouse =
                {
                "https://cdn.pixabay.com/photo/2016/11/29/03/53/house-1867187_1280.jpg",
                "https://cdn.pixabay.com/photo/2016/11/18/17/46/house-1836070_1280.jpg"
                };
            string[] imgBeigeHouse =
                {
                "https://cdn.pixabay.com/photo/2016/08/16/03/39/home-1597079_1280.jpg",
                "https://cdn.pixabay.com/photo/2016/08/16/03/50/exterior-1597098_1280.jpg"
                };
            string[] whiteHouse =
                {
                "https://cdn.pixabay.com/photo/2012/11/19/16/26/house-66627_960_720.jpg",
                "https://cdn.pixabay.com/photo/2016/08/26/15/06/home-1622401_1280.jpg"
                };
            string[] imgBrownHouse = {
                "https://cdn.pixabay.com/photo/2016/01/23/21/58/new-1158139_1280.jpg",
                "https://cdn.pixabay.com/photo/2017/07/09/03/19/home-2486092_1280.jpg"
                };

            picStringsHouse.AddRange(new List<string[]>
            {
                imgHouse, imgGreyHouse, imgBeigeHouse, whiteHouse, imgBrownHouse
            });



            List<string[]> picStringsApartment = new List<string[]>();

            string[] imgApartment =
                {
                "https://meltwater-apps-production.s3.eu-west-1.amazonaws.com/uploads/images/582addd000e4eb2b7dd9df7f/blobid4_1614761878277.png",
                "https://cdn.pixabay.com/photo/2014/08/11/21/39/wall-416060_1280.jpg"
                };
            string[] imgOldApartment =
                {
                "https://cdn.pixabay.com/photo/2015/07/08/10/29/appartment-building-835817_1280.jpg",
                "https://cdn.pixabay.com/photo/2017/02/24/12/22/kitchen-2094707_960_720.jpg"
                };
            string[] imgModernApart =
                {
                "https://cdn.pixabay.com/photo/2017/12/30/18/29/architecture-3050682_1280.jpg",
                "https://cdn.pixabay.com/photo/2018/01/26/08/15/dining-room-3108037_1280.jpg"
                };
            string[] imgModernWhiteApart =
                {
                "https://cdn.pixabay.com/photo/2015/11/06/11/48/multi-family-home-1026488_1280.jpg",
                "https://cdn.pixabay.com/photo/2024/04/09/14/18/ai-generated-8686094_1280.jpg"
                };
            string[] imgApart =
                {
                "https://cdn.pixabay.com/photo/2017/10/06/04/33/new-housing-development-2821969_1280.jpg",
                "https://cdn.pixabay.com/photo/2017/03/28/12/10/chairs-2181947_1280.jpg"
                };
            string[] imgHighApart =
                {
                "https://cdn.pixabay.com/photo/2018/03/24/03/45/structure-3255638_1280.jpg",
                "https://cdn.pixabay.com/photo/2017/03/25/23/32/kitchen-2174593_1280.jpg"
                };

            picStringsApartment.AddRange(new List<string[]>
            {
                imgApartment, imgOldApartment, imgModernApart, imgModernWhiteApart, imgApart, imgHighApart
            });



            List<string[]> picStringsTown = new List<string[]>();

            string[] imgTownhouse =
                {
                "https://www.bonava.se/siteassets/projekt/altadalen/radhus-pionen-1180x500.jpg",
                "https://cdn.pixabay.com/photo/2017/08/02/01/01/living-room-2569325_1280.jpg"
                };
            string[] imgTownBeige =
                {
                "https://cdn.pixabay.com/photo/2017/09/03/16/58/house-2711171_1280.jpg",
                "https://cdn.pixabay.com/photo/2020/11/24/11/36/bedroom-5772286_1280.jpg"
                };
            string[] imgTownBlue =
                {
                "https://cdn.pixabay.com/photo/2013/11/13/21/14/san-francisco-210230_1280.jpg",
                "https://cdn.pixabay.com/photo/2018/05/25/17/52/home-3429674_1280.jpg"
                };

            picStringsTown.AddRange(new List<string[]>
            {
                imgTownhouse, imgTownBeige, imgTownBlue
            });


            List<string[]> picStringsVacation = new List<string[]>();

            string[] imgVacationhome =
                {
                "https://www.stugaonline.se/thumb/586/1280x0/fellin2.jpg",
                "https://cdn.pixabay.com/photo/2024/04/21/01/27/ai-generated-8709663_1280.png"
                };
            string[] imgFarmHouse =
                {
                "https://cdn.pixabay.com/photo/2016/11/18/17/46/house-1836070_1280.jpg",
                "https://cdn.pixabay.com/photo/2013/07/04/18/33/western-143213_1280.jpg"
                };
            string[] imgSummerHouse =
                {
                "https://cdn.pixabay.com/photo/2017/11/16/19/29/cottage-2955582_1280.jpg",
                "https://cdn.pixabay.com/photo/2016/07/08/23/36/beach-house-1505461_1280.jpg"
                };
            string[] imgMountainHouse =
                {
                "https://cdn.pixabay.com/photo/2013/08/30/12/56/holiday-house-177401_1280.jpg",
                "https://cdn.pixabay.com/photo/2014/07/10/17/17/bedroom-389254_1280.jpg"
                };

            picStringsVacation.AddRange(new List<string[]>
            {
                imgVacationhome, imgFarmHouse, imgSummerHouse, imgMountainHouse
            });


            #endregion

            #region descriptions

            string[] descriptionHouse =
                {
                "Charmig villa med karaktär. Mysig känsla med originaldetaljer och öppen spis. Lugnt område nära bekvämligheter.",
                "Modern bondgård, öppen planlösning. Lantlig charm möter modern design i detta hus med öppen planlösning. Beläget i ett område med utmärkta skolor.",
                "Uppdaterat familjehem med pool. Välskött hus med rymliga utrymmen och privat pool för härliga sommardagar.",
                "Lyxig herrgård, panoramautsikt. Exklusivt boende med fantastisk vy över omgivningarna. Stor tomt ger gott om utrymme.",
                "Renoveringsobjekt med potential. Funktionell planlösning och bra läge erbjuder potential att renovera och skapa ditt drömhem."
                };
            string[] descriptionApartment =
                {
                "Mysig studio med stadsvy. Charmiga detaljer och generöst ljus flödar genom denna optimala lägenhet för singlar eller par.",
                "Modern enrummare med balkong. Stilren lägenhet med öppen planlösning och balkong erbjuder centralt boende med stadspuls.",
                "Tvättmaskin i lägenheten. Bekvämlighet och lugnt område nära kollektivtrafik gör denna lägenhet perfekt för en smidig vardag.",
                "Lyxig skyskrapa med takpool. Njut av fantastisk utsikt och hög standard i denna skyskrapa med takpool och conciergetjänster.",
                "Husdjursvänlig bottenvåning med uteplats. Mysig lägenhet på bottenvåningen med egen uteplats, perfekt för dig och din fyrbenta vän i ett djurvänligt område."
                };
            string[] descriptionTownhouse =
                {
                "Modernt radhus, underhållsfritt. Stilrent boende med öppen planlösning och egen uteplats. Underhållsfri fasad ger mer tid att njuta av hemmet.",
                "Ljust radhus i änden. Extra ljusinsläpp och bekvämt läge nära centrum gör detta radhus perfekt för den som uppskattar ljus och stadspuls.",
                "Familjevänligt radhus med pool. Öppen planlösning, inhägnad tomt och gemensam pool i området skapar en perfekt miljö för familjen.",
                "Lyxigt radhus med takterrass. Centralt läge möter hög standard och fantastisk utsikt från den privata takterrassen.",
                "Radhus nära gågata. Bekvämt boende inom promenadavstånd till restauranger, kultur och nöjen i en levande stadsmiljö."
                };
            string[] descriptionVacationhome =
                {
                "Strandstuga med havsutsikt. Vakna till vågornas brus i denna charmiga strandstuga. Njut av privat veranda och fantastisk havsutsikt. Perfekt för romantisk getaway eller familjesemester.",
                "Mysig stuga i bergen. Fly vardagens stress i denna mysiga stuga inbäddad bland bergen. Njut av lugnet i naturen, utforska vandringsleder och koppla av vid brasan. Perfekt för en avkopplande reträtt.",
                "Lyxig ski-in/ski-out lägenhet. Skidåkning direkt från dörren i denna lyxiga lägenhet. Njut av moderna bekvämligheter, privat balkong med fjällutsikt och uppvärmd pool för avkoppling efter skidåkning. Perfekt för vintersemester med vänner eller familj.",
                "Sjönära stuga med privat brygga. Skapa minnen vid sjön i denna charmiga stuga med privat brygga. Perfekt för fiske, båtutflykter och avkoppling i naturskön miljö.",
                "Strandnära villa med pool. Koppla av vid poolen eller njut av närheten till stranden i denna villa. Perfekt för familjesemester eller semester med vänner, med gott om utrymme och avkopplande atmosfär."
                };

            #endregion

            #region adresses

            string[] adresses =
            {
                "Ekbackavägen",
                "Stora Strandgatan",
                "Björkhagshöjden",
                "Parkvägen",
                "Smedjegatan",
                "Lilla Torget",
                "Kungshögavägen",
                "Fiskehamnsgränd",
                "Prästgatan",
                "Sätergläntan",
                "Gamla Landsvägen",
                "Snickargränd",
                "Kvarngatan",
                "Brogatan",
                "Backavägen",
                "Humlegången",
                "Södra Parkeringen",
                "Kyrkbacken",
                "Tallvägen",
                "Fiskargatan",
                "Backåkersvägen",
                "Stationsgatan",
                "Strandpromenaden",
                "Norra Hamnen",
                "Ekbacken",
                "Kaptensgatan",
                "Solrosvägen",
                "Prästgårdsvägen",
                "Lilla Torget",
                "Källargatan",
                "Kungsgatan",
                "Stationsvägen",
                "Skolgatan",
                "Badhusparken",
                "Skovägen",
                "Fiskargatan",
                "Strandvägen",
                "Skogsvägen"
            };

            #endregion

            #region salesObject generator

            if (!_appDbContext.SalesObjects.Any())
            {
                var categories = _appDbContext.Categories.ToList();
                var realtors = _appDbContext.Realtors.ToList();
                var counties = _appDbContext.Counties.ToList();

                var salesObjects = new List<SalesObject>();


                for (int i = 0; i < objectAmount; i++)
                {
                    //var randomCategory = Random.Shared.Next(0, categories.Count);
                    var category = categories[Random.Shared.Next(0, categories.Count)];
                    var realtor = realtors[Random.Shared.Next(0, realtors.Count)];

                    double daysAhead = (double)Random.Shared.Next(1, 31);
                    var viewingDates = new List<DateTime>() { DateTime.Now.AddDays(daysAhead) };

                    var price = Random.Shared.Next(1000000, 12000001);

                    salesObjects.Add(new SalesObject()
                    {
                        Category = category,
                        County = counties[Random.Shared.Next(0, counties.Count)],
                        Realtor = realtor,
                        CreatorName = $"{realtor.FirstName} {realtor.LastName}",
                        CreationDate = DateTime.Now,
                        Adress = $"{adresses[Random.Shared.Next(0, adresses.Length)]} {Random.Shared.Next(1, 51)}",
                        BuildYear = Random.Shared.Next(1960, 2025),
                        ListingPrice = price,
                        CurrentPrice = price,
                        ViewingDates = viewingDates
                    });

                    switch (category.Name)
                    {
                        case "Apartment":

                            salesObjects[i].Rooms = Random.Shared.Next(1, 6);
                            salesObjects[i].LivingArea = Random.Shared.Next(30, 101);
                            salesObjects[i].MonthlyFee = Random.Shared.Next(3000, 12001);
                            salesObjects[i].Level = Random.Shared.Next(0, 5);
                            salesObjects[i].Lift = Random.Shared.Next(2) == 0;
                            salesObjects[i].ObjectDescription = descriptionApartment[Random.Shared.Next(0, descriptionApartment.Length)];
                            //salesObjects[i].ImageLinks = imgApartment.ToList();
                            salesObjects[i].ImageLinks = picStringsApartment[Random.Shared.Next(0, picStringsApartment.Count)].ToList();
                            break;

                        case "House":

                            salesObjects[i].Rooms = Random.Shared.Next(3, 9);
                            salesObjects[i].LivingArea = Random.Shared.Next(60, 201);
                            salesObjects[i].AncillaryArea = Random.Shared.Next(61);
                            salesObjects[i].PlotArea = Random.Shared.Next(500, 5001);
                            salesObjects[i].YearlyCost = Random.Shared.Next(10000, 70001);
                            salesObjects[i].ObjectDescription = descriptionHouse[Random.Shared.Next(0, descriptionHouse.Length)];
                            //salesObjects[i].ImageLinks = imgHouse.ToList();
                            salesObjects[i].ImageLinks = picStringsHouse[Random.Shared.Next(0, picStringsHouse.Count)].ToList();
                            break;

                        case "TownHouse":

                            salesObjects[i].Rooms = Random.Shared.Next(3, 6);
                            salesObjects[i].LivingArea = Random.Shared.Next(60, 131);
                            salesObjects[i].MonthlyFee = Random.Shared.Next(5000, 15001);
                            salesObjects[i].AncillaryArea = Random.Shared.Next(31);
                            salesObjects[i].PlotArea = Random.Shared.Next(250, 501);
                            salesObjects[i].ObjectDescription = descriptionTownhouse[Random.Shared.Next(0, descriptionTownhouse.Length)];
                            //salesObjects[i].ImageLinks = imgHouse.ToList();
                            salesObjects[i].ImageLinks = picStringsTown[Random.Shared.Next(0, picStringsTown.Count)].ToList();
                            break;

                        case "VacationHome":

                            salesObjects[i].Rooms = Random.Shared.Next(3, 6);
                            salesObjects[i].LivingArea = Random.Shared.Next(20, 111);
                            salesObjects[i].AncillaryArea = Random.Shared.Next(51);
                            salesObjects[i].PlotArea = Random.Shared.Next(300, 5001);
                            salesObjects[i].ObjectDescription = descriptionVacationhome[Random.Shared.Next(0, descriptionVacationhome.Length)];
                            //salesObjects[i].ImageLinks = imgHouse.ToList();
                            salesObjects[i].ImageLinks = picStringsVacation[Random.Shared.Next(0, picStringsVacation.Count)].ToList();
                            break;

                        default:
                            Console.WriteLine("Unknown category");
                            break;
                    }
                }

                try
                {
                    await _appDbContext.SalesObjects.AddRangeAsync(salesObjects);
                    _appDbContext.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Något gick fel vid lagring av försäljningsobjekt till databasen!");
                }
            }
            #endregion

        }
    }
}

