using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FribergHomes.API.Data
{
    /* DataBase
     * Name: FribergHomesDB
     * @ Author : Rebecka 2024-04-15
     * @ Update: Using configuration to get connectionstring from appsettings.json / Rebecka 2024-04-16
     */
    public class ApplicationDBContext : DbContext
    {
        // Old code
        //public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        //{
        //}

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
        /*
         DbSet<House> Houses { get; set; }
        DbSet<Apartment> Apartments { get; set; }
         */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FribergHomesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            string? connectionString = _configuration.GetConnectionString("FribergHomesDB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
