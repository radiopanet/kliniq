using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kliniqQ.Domain.Entities;

namespace kliniqQ.Infrastructure.Persistence.Configurations;

public class StationConfiguration: IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.ToTable("station");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("station_id");

        builder.Property(s => s.StationName)
            .HasColumnName("station_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property.(s => s.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true); 
          
    }
}