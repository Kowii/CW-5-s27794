using Apbd3.Data;
using Apbd3.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Apbd3.Services;

public interface IDbService
{
    public Task<ICollection<PatientGetDto>> GetPatientsAsync();
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
            Birthdate = e.Birthdate
            //,
            /*
            Prescriptions = e.Prescriptions.Select( p => new PatientGetDtoPrescription
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctors = p.Doctor.Select(d => new PatientGetDtoPrescriptionDoctor
                {
                    FirstName = d.FirstName,
                    IdDoctor = d.IdDoctor
                }),
                Medicaments = p.PrescriptionMedicaments.Select(pm => new PatientGetDtoPrescriptionMedicament
                {
                    Description = pm.Medicament.Description,
                    IdMedicament = pm.IdMedicament,
                    Dose = pm.Dose,
                    Medicament = pm.Medicament,
                    Prescription = pm.Prescription
                })
            })
        });
    }
}
*/
        }).ToListAsync();
    }
}