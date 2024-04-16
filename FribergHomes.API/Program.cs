
using FribergHomes.API.Data;
using FribergHomes.API.Data.Interfaces;
using FribergHomes.API.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            /* DbContext
             * First it will look for a Database called FribergHomesDB,
             * if not found a exception will be thrown and a Database will get created.
             * @ Author: Rebecka 2024-04-15
             * @ Update: The code i thought about didnt work smooth, so i changed to get use sqlserver -> connectionstring. / Rebecka 2024-04-16
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
            builder.Services.AddTransient<ICategory, CategoryRepository>(); // Reb


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            


            var app = builder.Build();

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
