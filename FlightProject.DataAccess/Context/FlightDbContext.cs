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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker Entityler üzerinde yapılan değişikliklerin veya yeni eklenen verinin yakalanmasını sağlayan propertydir.
            //Update operasyonlarında track edilen verileri yakalayıp elde etmemeizi sağlar.
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
