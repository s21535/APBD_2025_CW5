using System.ComponentModel.DataAnnotations;

namespace Lab5MedicalApp.API.Models.Domain;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    
    public ICollection<Perscription> Perscriptions { get; set; }
}