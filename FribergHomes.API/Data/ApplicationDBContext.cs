using FribergHomes.API.Constants;
using FribergHomes.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FribergHomes.API.Data
{
    /* DataBase
     * Name: FribergHomesDB
     * @ Author : Rebecka 2024-04-15
     * @ Update: Using configuration to get connectionstring from appsettings.json / Rebecka 2024-04-16
     * @ Update:
     */

    public class ApplicationDBContext : IdentityDbContext<Realtor>
    {
       
        private readonly IConfiguration _configuration;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<SalesObject> SalesObjects { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FribergHomesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            string? connectionString = _configuration.GetConnectionString("FribergHomesDB");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(

                 new IdentityRole
                 {
                     Name = ApiRoles.Admin,
                     NormalizedName = ApiRoles.Admin.ToUpper(),
                     Id = "dd543f41-7a25-4b52-a51d-4d7681bcfa87"
                 },

                new IdentityRole
                {
                    Name = ApiRoles.Realtor,
                    NormalizedName = ApiRoles.Realtor.ToUpper(),
                    Id = "e13e3b26-67e0-425a-b8ae-54ad6a1b1c09"
                }); ;


        }
    }
}
