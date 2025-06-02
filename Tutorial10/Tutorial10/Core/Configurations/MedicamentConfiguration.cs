using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial10.Core.Models;

namespace Tutorial10.Core.Configurations;

public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicament");
        builder.HasKey(e => e.IdMedicament);
        builder.Property(e => e.IdMedicament)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(e => e.PrescriptionMedicaments)
            .WithOne(e => e.Medicament)
            .HasForeignKey(e => e.IdMedicament)
            .OnDelete(DeleteBehavior.Cascade);
    }
}