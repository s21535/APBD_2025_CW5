using System.ComponentModel.DataAnnotations;

namespace Lab5MedicalApp.API.Models.Domain;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
    
    public ICollection<Perscription> Perscriptions { get; set; }
}