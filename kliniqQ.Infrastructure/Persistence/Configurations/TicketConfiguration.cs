using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kliniqQ.Domain.Entities;

namespace kliniqQ.Infrastructure.Persistence.Configurations;

public class TicketConfiguration: IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("ticket");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("ticket_id");

        builder.Property(t => t.TicketNumber)
            .HasColumnName("ticket_number")
            .HasMaxLength(10)
            .IsRequired();

                builder.Property(t => t.IssuedAt)
            .HasColumnName("issued_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(t => t.CalledAt)
            .HasColumnName("called_at");

        builder.Property(t => t.ServiceStartAt)
            .HasColumnName("service_start_at");

        builder.Property(t => t.ServiceEndAt)
            .HasColumnName("service_end_at");

        builder.Property(t => t.IssuedDate)
            .HasColumnName("issued_date")
            .HasDefaultValueSql("CURRENT_DATE");

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.HasIndex(t => new { t.PatientId, t.IssuedDate })
            .IsUnique();

        builder.HasIndex(t => new { t.TicketNumber, t.IssuedDate })
            .IsUnique();

        builder.HasOne(t => t.Patient)
            .WithMany()
            .HasForeignKey(t => t.PatientId)
            .IsRequired();

        builder.HasOne(t => t.Station)
            .WithMany()
            .HasForeignKey(t => t.StationId);

        builder.HasOne(t => t.AssignedNurse)
            .WithMany()
            .HasForeignKey(t => t.AssignedNurseId);

        builder.ToTable(tb => tb.HasCheckConstraint(
            "valid_times",
            "(called_at IS NULL OR called_at >= issued_at) AND " +
            "(service_start_at IS NULL OR service_start_at >= called_at) AND " +
            "(service_end_at IS NULL OR service_end_at >= service_start_at)"
        )); 
    }
}