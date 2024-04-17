using FribergHomes.API.Data;
using FribergHomes.API.Models;

namespace FribergHomes.API.Seeders
{
    public class AgencySeeder 
    {
        public async Task SeedAgencies(ApplicationDBContext appDBctx)
        {
            if (!appDBctx.Agencies.Any())
            {
                await appDBctx.AddRangeAsync(
                    new Agency
                    {
                        Name = "Fastighetsbyrån",
                        Presentation = "Idag är vi Sveriges ledande mäklarkedja med ca 270 kontor och cirka 1 700 medarbetare över hela landet och i delar av Spanien och Portugal. " +
                            "Tillsammans förmedlar vi omkring en fjärdedel av Sveriges bostadsaffärer. Men vi har inget intresse av att slå oss till ro, att ständigt vilja utvecklas " +
                            "är en självklar del av att arbeta på Fastighetsbyrån. Den inställningen bottnar i vår vision: \"Vi vill vara det mest rekommenderade företaget i Sverige. " +
                            "Vi sätter rekommendationen i centrum eftersom det är det yttersta beviset på att kunden upplevt något positivt\".",
                        Logo = "https://yt3.googleusercontent.com/VUvkdg2e4NLTbAZJywQLJVGQ8VysRfPOiHGH3FxTkIIIATGhYGERC3mymkKjYWluHJ7c-Alr=s900-c-k-c0x00ffffff-no-rj"
                    },
                     new Agency
                     {
                         Name = "HusmanHagberg",
                         Presentation = "HusmanHagberg är en av landets ledande fastighetsmäklarkedjor med över 100 kontor och drygt 400 medarbetare i både Sverige och Spanien. " +
                            "Vi är privatägda och fristående från banker och försäkringsbolag. Många av våra medarbetare bor i området där de arbetar. Med ett äkta engagemang och " +
                            "en passion för sitt yrke vinner de kundernas hjärtan. Det är därför vi är fastighetsmäklaren med nöjdare kunder. Välkommen att bli nöjd du också!",
                         Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTHGocnnozEK9pG6eVx8RxfGWrebNMWC5FCqj6FPbXEkQ&s"
                     },
                     new Agency
                     {
                         Name = "SkandiaMäklarna",
                         Presentation = "Vi tänker inte som andra mäklare. Vi vet att det vi förmedlar är mycket mer än bara objekt, oavsett om du vill sälja eller köpa. " +
                            "Vi brukar säga att vi hjälper människor att hitta hem, och det är vi stolta över. Att få hjälpa till i din viktigaste affär. Så att den blir " +
                            "bättre för dig. Det har vi gjort i mer än fyrtio år, så idag finns vi på närmare 100 kontor i Sverige, Spanien och Portugal. " +
                            "Funderar du på att sälja din bostad, eller vill du hitta något nytt? Har det blivit dags för ett fritidshus, eller drömmer du kanske om att bo utomlands? " +
                            "Kontakta oss så hjälper vi dig med. Välkommen hem. Välkommen till SkandiaMäklarna.",
                         Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQBPdskHzjI78aPKbX95OXfgmUQSp7yZNcPCP90DyQF-A&s"
                     },
                     new Agency
                     {
                         Name = "Mäklarhuset",
                         Presentation = "Mäklarhuset är ett av Sveriges största, icke franchisedrivna, privata mäklarföretag med cirka 110 lokala fristående kontor runt om i hela Sverige " +
                         "och delar av Spanien. Vi äger oss själva vilket stärker det lokala entreprenörskapet, lokalt engagemang och möjligheten att anpassa verksamheten utifrån varje marknad. " +
                         "Det vi brinner för är att hitta den bästa lösningen för just dig. För att när våra kunder lyckas, lyckas vi.",
                         Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTK-LgA8dU4zUS1EWbg1s1Hlf3DhNlmVY4B2OJFPVjxzA&s"
                     },
                     new Agency
                     {
                         Name = "Bjurfors",
                         Presentation = "Bjurfors är Sveriges mest rekommenderade mäklare, enligt en färsk undersökning från Svenskt Kvalitetsindex. Våra kunder är så nöjda med sina bostadsaffärer " +
                            "med oss att de mer än gärna rekommenderar oss vidare till en vän eller bekant. Det är vi väldigt stolta över. Vi brinner för att göra både våra säljare och köpare nöjda. " +
                            "För oss är det viktigt att varje kund får den premiumupplevelse den förtjänar, oavsett bostad och marknadsläge.",
                         Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRKQs8ivCV31qYxdGIiDEydwbPYif8RjuGr9Lxc326_w&s"
                     });
                await appDBctx.SaveChangesAsync();
            }
        }
    }
}
