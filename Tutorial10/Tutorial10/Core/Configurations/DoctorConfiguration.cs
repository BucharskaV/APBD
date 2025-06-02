using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Core.Models;

namespace Tutorial10.Core.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");
        builder.HasKey(e => e.IdDoctor);
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("FirstName");
        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(e => e.Prescriptions)
            .WithOne(e => e.Doctor)
            .HasForeignKey(e => e.IdDoctor)
            .OnDelete(DeleteBehavior.Cascade);
    }
}