using Microsoft.EntityFrameworkCore;

namespace Tutorial10.Core.Models;

public class ClinicDBContext : DbContext
{
    public ClinicDBContext(DbContextOptions<ClinicDBContext> options) : base(options) { }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");
            entity.HasKey(e => e.IdDoctor);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("FirstName");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasMany(e => e.Prescriptions)
                .WithOne(e => e.Doctor)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");
            entity.HasKey(e => e.IdPatient);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("FirstName");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DateOfBirth)
                .IsRequired()
                .HasColumnType("datetime");
            entity.HasMany(e => e.Prescriptions)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Patient");
            entity.HasKey(e => e.IdPrescription);
            entity.Property(e => e.IdPrescription)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate)
                .IsRequired()
                .HasColumnType("datetime");
        });
        
        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.ToTable("Medicament");
            entity.HasKey(e => e.IdMedicament);
            entity.Property(e => e.IdMedicament)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasMany(e => e.PrescriptionMedicaments)
                .WithOne(e => e.Medicament)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.ToTable("PrescriptionMedicament");
            entity.HasKey(e => new {e.IdPrescription, e.IdMedicament});
            entity.Property(e => e.Details)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}