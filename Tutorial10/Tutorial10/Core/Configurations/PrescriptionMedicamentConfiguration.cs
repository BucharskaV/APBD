using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Core.Models;

namespace Tutorial10.Core.Configurations;

public class PrescriptionMedicamentConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder.ToTable("PrescriptionMedicament");
        builder.HasKey(e => new {e.IdPrescription, e.IdMedicament});
        builder.Property(e => e.Details)
            .IsRequired()
            .HasMaxLength(100);
    }
}