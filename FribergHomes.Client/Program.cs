using FribergHomes.Client.Services;
using FribergHomes.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FribergHomes.Client
{
    public class Program
    {
        /* Configured HttpClient base adress // Tobias 2024-04-23 
        */

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7161/") });

            builder.Services.AddTransient<IAgencyService, AgencyService>(); // Sanna 2024-04-24
            builder.Services.AddTransient<IRealtor, RealtorService>(); // Reb 2024-04-24
            builder.Services.AddTransient<ICounty, CountyService>(); // Reb 2024-04-25
            builder.Services.AddTransient<ISalesObject, SalesObjectService>(); // Tobias 2024-04-25
            builder.Services.AddTransient<ICategory, CategoryService>(); // Rebecka 2024-04-26

            builder.Services.AddBlazorBootstrap();




            await builder.Build().RunAsync();
        }
    }
}
