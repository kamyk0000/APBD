using DBApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBApp.Context;

public class MyDb : DbContext
{
    public MyDb()
    {
    }

    public MyDb(DbContext context)
    {
    }
    
    public MyDb(DbContextOptions options)
        : base(options)
    {
    }
    
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicament>(m =>
        {
            m.HasKey(et => et.IdMedicament);
            m.Property(p => p.Name).HasMaxLength(100).IsRequired();
            m.Property(p => p.Description).HasMaxLength(100).IsRequired();
            m.Property(p => p.Type).HasMaxLength(100).IsRequired();
        }
        );
        modelBuilder.Entity<Patient>(m =>
            {
                m.HasKey(et => et.IdPatient);
                m.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                m.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                m.Property(p => p.Birthdate).IsRequired();
                m.HasMany(p => p.Prescriptions).WithOne(p => p.Patient).HasForeignKey(p => p.IdPatient);
            }
        );
        modelBuilder.Entity<Doctor>(m =>
            {
                m.HasKey(et => et.IdDoctor);
                m.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                m.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                m.Property(p => p.Email).HasMaxLength(100).IsRequired();
                m.HasMany(p => p.Prescriptions).WithOne(p => p.Doctor).HasForeignKey(p => p.IdDoctor);
            }
        );
        modelBuilder.Entity<Prescription>(m =>
            {
                m.HasKey(et => et.IdPrecription);
                m.Property(p => p.Date).IsRequired();
                m.Property(p => p.DueDate).IsRequired();
            }
        );
        modelBuilder.Entity<Prescription_Medicament>(m =>
            {
                m.HasKey(et => et.IdMedicament);
                m.HasKey(et => et.IdPrescription);
                m.Property(p => p.Dose);
                m.Property(p => p.Details).HasMaxLength(100).IsRequired();
            }
        );
    }
}