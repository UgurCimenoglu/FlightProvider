using FlightProject.Core.Entities;
using FlightProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FlightProject.DataAccess.Context
{
    public class FlightDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=FlightDb;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<PassengerInformation> PassengerInformations { get; set; }
    }
}
