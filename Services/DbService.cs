using Apbd3.Data;
using Apbd3.DTOs;

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
            Birthdate = e.Birthdate,
            Prescriptions = e.Prescriptions.Select( p => new PatientGetDtoPrescription
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Medicaments = p.Medicaments.Select
            })
        });
    }
}