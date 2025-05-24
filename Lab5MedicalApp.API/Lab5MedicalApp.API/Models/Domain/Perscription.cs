using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5MedicalApp.API.Models.Domain;

public class Perscription
{
    [Key]
    public int IdPerscription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    
    public ICollection<PerscriptionMedicament> PerscriptionMedicaments { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}