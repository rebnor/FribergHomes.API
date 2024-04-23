using FribergHomes.API.Data;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FribergHomes.API.Seeders
{
    /* Seeder class that checks if DB table SalesObjects contains any entries.
     * If not, creates new SalesObject objects and populates the DB table.
     * Author: Tobias 2024-04-19
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
            #region pictures

            string[] imgHouse = 
                {
                "https://www.a-hus.se/storage/4CBEE1AB262DB926EFE91C41ACBF0CB8D09D3B6B01D420DDF5018865C8AA0E48/579cece4bb5e47da88993b28b4484aae/jpg/media/512f835bc0194897ae17b63334635b67/Bj%C3%B6rk%C3%B6%20Klassisk1920x1920.jpg"
                };
            string[] imgApartment = 
                {
                "https://meltwater-apps-production.s3.eu-west-1.amazonaws.com/uploads/images/582addd000e4eb2b7dd9df7f/blobid4_1614761878277.png"
                };
            string[] imgTownhouse = 
                {
                "https://www.bonava.se/siteassets/projekt/altadalen/radhus-pionen-1180x500.jpg"
                };
            string[] imgVacationhome = 
                {
                "https://www.stugaonline.se/thumb/586/1280x0/fellin2.jpg"
                };

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
                            salesObjects[i].ImageUrls = imgApartment.ToList();
                            break;

                        case "House":

                            salesObjects[i].Rooms = Random.Shared.Next(3, 9);
                            salesObjects[i].LivingArea = Random.Shared.Next(60, 201);
                            salesObjects[i].AncillaryArea = Random.Shared.Next(61);
                            salesObjects[i].PlotArea = Random.Shared.Next(500, 5001);
                            salesObjects[i].YearlyCost = Random.Shared.Next(10000, 70001);
                            salesObjects[i].ObjectDescription = descriptionHouse[Random.Shared.Next(0, descriptionHouse.Length)];
                            salesObjects[i].ImageUrls = imgHouse.ToList();
                            break;

                        case "TownHouse":

                            salesObjects[i].Rooms = Random.Shared.Next(3, 6);
                            salesObjects[i].LivingArea = Random.Shared.Next(60, 131);
                            salesObjects[i].MonthlyFee = Random.Shared.Next(5000, 15001);
                            salesObjects[i].AncillaryArea = Random.Shared.Next(31);
                            salesObjects[i].PlotArea = Random.Shared.Next(250, 501);
                            salesObjects[i].ObjectDescription = descriptionTownhouse[Random.Shared.Next(0, descriptionTownhouse.Length)];
                            salesObjects[i].ImageUrls = imgHouse.ToList();
                            break;

                        case "VacationHome":

                            salesObjects[i].Rooms = Random.Shared.Next(3, 6);
                            salesObjects[i].LivingArea = Random.Shared.Next(20, 111);
                            salesObjects[i].AncillaryArea = Random.Shared.Next(51);
                            salesObjects[i].PlotArea = Random.Shared.Next(300, 5001);
                            salesObjects[i].ObjectDescription = descriptionVacationhome[Random.Shared.Next(0, descriptionVacationhome.Length)];
                            salesObjects[i].ImageUrls = imgHouse.ToList();
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

