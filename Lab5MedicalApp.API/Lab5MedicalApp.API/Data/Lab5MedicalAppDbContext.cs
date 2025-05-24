using Lab5MedicalApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lab5MedicalApp.API.Data;

public class Lab5MedicalAppDbContext : DbContext
{
    public Lab5MedicalAppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PerscriptionMedicament> PerscriptionMedicaments { get; set; }
    public DbSet<Perscription> Perscriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kompozytowy klucz główny dla tabeli łącznikowej
        modelBuilder.Entity<PerscriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPerscription });

        modelBuilder.Entity<PerscriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PerscriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        modelBuilder.Entity<PerscriptionMedicament>()
            .HasOne(pm => pm.Perscription)
            .WithMany(p => p.PerscriptionMedicaments)
            .HasForeignKey(pm => pm.IdPerscription);

        // Relacja: Doctor 1 - * Perscriptions
        modelBuilder.Entity<Perscription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Perscriptions)
            .HasForeignKey(p => p.IdDoctor)
            .OnDelete(DeleteBehavior.Restrict); // lub .Cascade, zależnie od potrzeb

        // Relacja: Patient 1 - * Perscriptions
        modelBuilder.Entity<Perscription>()
            .HasOne(p => p.Patient)
            .WithMany(pa => pa.Perscriptions)
            .HasForeignKey(p => p.IdPatient)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}