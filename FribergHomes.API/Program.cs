
using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Data.Repositories;
using FribergHomes.API.Seeders;
using Microsoft.EntityFrameworkCore;

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




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Seed Agencies / Reb 2024-04-17
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDBContext>();

                var seedAgencies = new AgencySeeder();
                await seedAgencies.SeedAgencies(dbContext);

                //RealtorSeeder added by Sanna 
                //var seedRealtors = new RealtorSeeder();
                //await seedRealtors.SeedRealtors(dbContext);
                
            }




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
