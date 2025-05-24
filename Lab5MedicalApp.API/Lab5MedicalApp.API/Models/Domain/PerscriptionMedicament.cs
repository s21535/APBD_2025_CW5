using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5MedicalApp.API.Models.Domain;

public class PerscriptionMedicament
{
    [Key][ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    [Key][ForeignKey(nameof(Perscription))]
    public int IdPerscription { get; set; }
    
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}