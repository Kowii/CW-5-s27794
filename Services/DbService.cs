using Apbd3.Data;
using Apbd3.DTOs;
using Apbd3.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd3.Services;

public interface IDbService
{
    public Task<ICollection<PatientGetDto>> GetPatientsAsync();
    public Task<PatientGetDtoPrescription> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData);
    public Task<PatientGetDto> GetPatientsAsyncById(int PatientId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<PatientGetDto>> GetPatientsAsync()
    {
        return await data.Patients.Select(e => new PatientGetDto
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.Birthdate,
            Prescriptions = e.Prescriptions.Select(pr => new PatientGetDtoPrescription
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Doctor = new PatientGetDtoDoctor
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName
                },
                Medicaments = pr.PrescriptionMedicaments.Select(md => new PatientGetDtoMedicament
                {
                    IdMedicament = md.IdMedicament,
                    Name = md.Medicament.Name,
                    Description = md.Medicament.Description,
                    Dose = md.Dose
                }).ToList()
            }).ToList()
            
        }).ToListAsync();
    }

    public async Task<PatientGetDtoPrescription> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData)
    {

        if (prescriptionData.Medicaments.Count > 10)
        {
            throw new Exception($"Added {prescriptionData.Medicaments.Count} when max is 10");
        }
        if (prescriptionData.Patient != null)
        {
            var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == prescriptionData.Patient.IdPatient);
            if (patient == null)
            {
                data.Patients.Add(new Patient
                {
                    Birthdate = prescriptionData.Patient.Birthdate,
                    FirstName = prescriptionData.Patient.FirstName,
                    LastName = prescriptionData.Patient.LastName
                });
            }
        }

        if (prescriptionData.DueDate < prescriptionData.Date)
        {
            throw new Exception("Due date cannot be before date");
        }

        List<Medicament> medicaments = new List<Medicament>();
        foreach (PatientGetDtoMedicament medicament in prescriptionData.Medicaments)
        {
            var med = await data.Medicaments.FirstOrDefaultAsync
                (m => m.IdMedicament == medicament.IdMedicament);
            if (med == null)
            {
                throw new Exception("Medicament not found");
            }
            medicaments.Add(med);
        }
        
        var prescription = new Prescription
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdDoctor = prescriptionData.Doctor.IdDoctor,
            IdPatient = prescriptionData.Patient.IdPatient
        };
        foreach (Medicament medicament in medicaments)
        {
            data.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = prescriptionData.IdPrescription
            });
        }
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();

        return new PatientGetDtoPrescription
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new PatientGetDtoDoctor
            {
                IdDoctor = prescription.Doctor.IdDoctor,
                FirstName = prescription.Doctor.FirstName
            },
            Medicaments = prescription.PrescriptionMedicaments.Select(md => new PatientGetDtoMedicament
            {
                IdMedicament = md.IdMedicament,
                Name = md.Medicament.Name,
                Description = md.Medicament.Description,
                Dose = md.Dose
            }).ToList()
        };

    }

    public async Task<PatientGetDto> GetPatientsAsyncById(int patientId)
    {
        var patient =  await data.Patients.Select(e => new PatientGetDto
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.Birthdate,
            Prescriptions = e.Prescriptions.Select(pr => new PatientGetDtoPrescription
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Doctor = new PatientGetDtoDoctor
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName
                },
                Medicaments = pr.PrescriptionMedicaments.Select(md => new PatientGetDtoMedicament
                {
                    IdMedicament = md.IdMedicament,
                    Name = md.Medicament.Name,
                    Description = md.Medicament.Description,
                    Dose = md.Dose
                }).ToList()
            }).ToList()
            
        }).FirstOrDefaultAsync(e => e.IdPatient == patientId);
        return patient ?? throw new InvalidOperationException();
    }
}
