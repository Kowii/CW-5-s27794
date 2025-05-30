using Apbd3.Models;

namespace Apbd3.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public ICollection<PatientGetDtoPrescription> Prescriptions { get; set; }
}

public class PatientGetDtoPrescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<PatientGetDtoMedicament> Medicaments { get; set; }
    public PatientGetDtoDoctor Doctor { get; set; }
}


public class PatientGetDtoMedicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
}

public class PatientGetDtoDoctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}