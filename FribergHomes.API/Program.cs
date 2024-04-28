
using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Data.Repositories;
using FribergHomes.API.Seeders;
using FribergHomes.API.Mappers;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace FribergHomes.API
{
    public class Program
    {      

        //public static void Main(string[] args)
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            /* DbContext
             * First it will look for a Database called FribergHomesDB,
             * if not found a exception will be thrown and a Database will get created.
             * @ Author: Rebecka 2024-04-15
             * @ Update: The code i thought about didnt+ work smooth, so i changed to get use sqlserver -> connectionstring. / Rebecka 2024-04-16
             */
            // Old code:
            //builder.Services.AddDbContext<ApplicationDBContext>(options =>
            //{
            //    // Looking for Database FribergHomesDB
            //    var connectionString = builder.Configuration.GetConnectionString("FribergHomesDB");
            //    //if not found
            //    if (connectionString == null)
            //    {
            //        throw new InvalidOperationException("Connection string 'FribergHomesDB' not found.");
            //    }
            //    // If found, the connection starts
            //    options.UseSqlServer(connectionString);

            //    // If no database called FribergHomesDB is found, it will create a Database with that name.
            //    var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>().UseSqlServer(connectionString).Options;
            //    using (var context = new ApplicationDBContext(dbContextOptions))
            //    {
            //        context.Database.EnsureCreated();
            //    }
            //});

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FribergHomesDB") ?? throw new InvalidOperationException("Connection string 'FribergHomesDB' not found.")));


            // Repositories
            builder.Services.AddTransient<ICounty, CountyRepository>(); // Reb
            builder.Services.AddTransient<ISalesObject, SalesObjectRepository>(); // Reb
            builder.Services.AddTransient<IAgency, AgencyRepository>(); // Sanna 
            builder.Services.AddTransient<IRealtor, RealtorRepository>(); // Tobias
            builder.Services.AddTransient<ICategory, CategoryRepository>(); // Reb

            builder.Services.AddAutoMapper(typeof(SalesObjectProfile)); // Tobias

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

          
            var app = builder.Build();

            // Seeders  / Reb 2024-04-17
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDBContext>();

                // Seed agenies /Rebecka 2024-04-17
                var seedAgencies = new AgencySeeder();
                await seedAgencies.SeedAgencies(dbContext);

                // County seeder /Tobias 2024-04-18
                var config = services.GetRequiredService<IConfiguration>();
                var countySeeder = new CountySeeder(dbContext, config);
                await countySeeder.SeedCounties();

                //RealtorSeeder added by Sanna 2024-04-18
                var seedRealtors = new RealtorSeeder();
                await seedRealtors.SeedRealtors(dbContext);

                //CategorySeeder added by Sanna 2024-04-18
                var seedCategories = new CategorySeeder();
                await seedCategories.SeedCategories(dbContext);

                // SalesObject seeder /Tobias 2024-04-19
                //SeedSalesObject parameters: int objectAmount (amount of objects to generate and store to DB).
                var salesObjectSeeder = new SalesObjectSeeder(dbContext);
                await salesObjectSeeder.SeedSalesObjects(100);
            }

          
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Added and configured CORS middleware  // Tobias 2024-04-23
            app.UseCors(policy =>
            {
                policy.WithOrigins("https://localhost:7196", "http://localhost:5083", "https://localhost:7161/")
                .AllowAnyHeader()
                .WithHeaders(HeaderNames.ContentType);
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
