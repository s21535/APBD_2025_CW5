namespace Lab5MedicalApp.API.Models.DTOs;

public class AddPrescriptionRequest
{
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }

    public DoctorDto Doctor { get; set; }
    public PatientDto Patient { get; set; }

    public List<MedicamentDto> Medicaments { get; set; }

    public class DoctorDto
    {
        public int IdDoctor { get; set; }
    }

    public class PatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class MedicamentDto
    {
        public int IdMedicament { get; set; }
        public int? Dose { get; set; }
        public string Details { get; set; }
    }
}