using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RyazanTrip.DataAccess.Postgres;

public class DesignTimeDbContextFactory 
   : IDesignTimeDbContextFactory<RyazanTripDbContext>
{
   public RyazanTripDbContext CreateDbContext(string[] args)
   {
      var optionsBuilder = new DbContextOptionsBuilder<RyazanTripDbContext>();

      optionsBuilder.UseNpgsql(
         "Host=localhost;Port=5432;Database=myapp;Username=admin;Password=admin"
      );

      return new RyazanTripDbContext(optionsBuilder.Options);
   }
}