using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity> {
   public void Configure(EntityTypeBuilder<RoleEntity> builder) {
      builder.ToTable("roles");

      builder.HasKey(x => x.RoleId);

      builder.Property(x => x.Name)
         .HasMaxLength(64);

      builder.HasData(
         new RoleEntity { RoleId = 1, Name = "User" },
         new RoleEntity { RoleId = 2, Name = "Admin" }
      );
      
   }
}