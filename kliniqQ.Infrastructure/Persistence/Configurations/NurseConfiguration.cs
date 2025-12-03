using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kliniqQ.Domain.Entities;

namespace kliniqQ.Infrastructure.Persistence.Configurations;

public class NurseConfiguration: IEntityTypeConfiguration<Nurse>
{
    public void Configure(EntityTypeBuilder<Nurse> builder)
    {
        builder.ToTable("nurse");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .HasColumnName("nurse_id");

        builder.Property(n => n.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(n => n.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(n => n.EmployeeNumber)
            .HasColumnName("employee_number")
            .IsRequired();

        builder.Property(n => n.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        builder.Property(n => n.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()");

        builder.Property(n => n.CurrentStationId)
            .HasColumnName("current_station_id");

        
        builder.HasOne(n => n.CurrentStation)
                .WithMany()
                .HasForeignKey(n => n.CurrentStationId)
                .OnDelete(DeleteBehavior.Restrict);
             

    }
}