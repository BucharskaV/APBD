using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Core.Models;

namespace Tutorial10.Core.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescription");
        builder.HasKey(e => e.IdPrescription);
        builder.Property(e => e.IdPrescription)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Date)
            .IsRequired()
            .HasColumnType("datetime");
        builder.Property(e => e.DueDate)
            .IsRequired()
            .HasColumnType("datetime");
    }
}