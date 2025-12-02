using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kliniqQ.Domain.Entities;

public class PatientConfiguration: IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityBuilder<Patient> builder)
    {
        builder.ToTable("patient");

        builder.HasKey(p => p.Id);

        bulder.Property(p => p.Id)
            .HasColumnName("patient_id");

        builder.Property(p =>p.NationalId)
            .HasColumnName("national_id")
            .HasMaxLength(13);

        builder.HasIndex(p => p.NationalId)
            .IsUnique();

        builder.Property(p => p.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20);

        builder.Property(p => p.Gender)
            .HasColumnName("gender")
            .HasMaxLength(20);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()");                     
    }
}