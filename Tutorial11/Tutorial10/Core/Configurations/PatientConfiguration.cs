using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Core.Models;

namespace Tutorial10.Core.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");
        builder.HasKey(e => e.IdPatient);
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("FirstName");
        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.DateOfBirth)
            .IsRequired()
            .HasColumnType("datetime");
        builder.HasMany(e => e.Prescriptions)
            .WithOne(e => e.Patient)
            .HasForeignKey(e => e.IdPatient)
            .OnDelete(DeleteBehavior.Cascade);
    }
}