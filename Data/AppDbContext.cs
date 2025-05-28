using Apbd3.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd3.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "Kacper",
            LastName = "Kowalski",
            Birthdate = new DateTime(1999, 05, 28)
        };
        var doctor = new Doctor
        {
            IdDoctor = 1,
            FirstName = "Marek",
            LastName = "Doe",
            Email = "em@gmain.co"
        };
        var medicament = new Medicament
        {
            IdMedicament = 1,
            Name = "Paracetamol",
            Description = "Paracetamollolololol",
            Type = "Antybiotyczne"
        };
        var prescripion = new Prescription
        {
            IdPrescription = 1,
            IdPatient = 1,
            IdDoctor = 1,
            Date = new DateTime(2025, 05, 28),
            DueDate = new DateTime(2025, 06, 28)
        };
        var prescriptionMedicament = new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 10,
            Details = "Paracetamol 1 na dzien"
        };
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(prescripion);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament);  
    }
}