
using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Data.Repositories;
using FribergHomes.API.Seeders;
using FribergHomes.API.Mappers;
using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FribergHomes.API
{
    public class Program
    {
        //private static UserManager<Realtor> _userManager;

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

            // Identity
            builder.Services.AddIdentityCore<Realtor>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>();

            // Identity - Password and account requirements
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });


            // Repositories
            builder.Services.AddTransient<ICounty, CountyRepository>(); // Reb
            builder.Services.AddTransient<ISalesObject, SalesObjectRepository>(); // Reb
            builder.Services.AddTransient<IAgency, AgencyRepository>(); // Sanna 
            builder.Services.AddTransient<IRealtor, RealtorRepository>(); // Tobias
            builder.Services.AddTransient<ICategory, CategoryRepository>(); // Reb

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // Tobias

            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                };
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Added and configured CORS middleware  // Tobias 2024-04-23
            app.UseCors(policy =>
            {
                policy.WithOrigins("https://localhost:7196", "http://localhost:5083", "https://localhost:7161")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            // Seeders  / Reb 2024-04-17
            // Added UserManager as a service, it's needed to seed Realtors and SalesObjects / Sanna 

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDBContext>();

                //Realtor seeder / Sanna 
                var userManager = services.GetRequiredService<UserManager<Realtor>>();             
                RealtorSeeder realtorSeeder = new RealtorSeeder();
                await realtorSeeder.SeedRealtors(dbContext, userManager);

                // Seed agencies /Rebecka 2024-04-17
                var seedAgencies = new AgencySeeder();
                await seedAgencies.SeedAgencies(dbContext);

                // County seeder /Tobias 2024-04-18
                var config = services.GetRequiredService<IConfiguration>();
                var countySeeder = new CountySeeder(dbContext, config);
                await countySeeder.SeedCounties();

                //CategorySeeder / Sanna 2024-04-18
                var seedCategories = new CategorySeeder();
                await seedCategories.SeedCategories(dbContext);

                // SalesObject seeder /Tobias 2024-04-19
                //SeedSalesObject parameters: int objectAmount (amount of objects to generate and store to DB).                
                var salesObjectSeeder = new SalesObjectSeeder(dbContext);
                await salesObjectSeeder.SeedSalesObjects(100, userManager);

            }

            app.MapControllers();

            app.Run();
        }
    }
}
