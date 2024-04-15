using FribergHomes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomes.API.Data
{
    /* DataBase
     * Name: FribergHomesDB
     * @ Author : Rebecka 2024-04-15
     */
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        DbSet<Category> Categories { get; set; }
        DbSet<County> Counties { get; set; }
        DbSet<Realtor> Realtors { get; set; }
        DbSet<Agency> Agencies { get; set; }

        // Old code
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FribergHomesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}
    }
}
