using System.ComponentModel.DataAnnotations;

namespace Apbd3.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public PrescriptionCreateDtoPatient Patient { get; set; }
    [Required]
    public ICollection<PatientGetDtoMedicament> Medicaments { get; set; }
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public PatientGetDtoDoctor Doctor { get; set; }
    
}
public class PrescriptionCreateDtoPatient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
}
public class PrescriptionCreateDtoMedicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
    
}
