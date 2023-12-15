using FlightProject.Core.Entities;
using FlightProject.DataAccess.Extensions;
using FlightProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlightProject.DataAccess.Context
{
    public class FlightDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
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
