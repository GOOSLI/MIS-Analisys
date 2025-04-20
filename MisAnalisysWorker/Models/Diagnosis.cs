using System.ComponentModel.DataAnnotations;

namespace MisAnalisysWorker.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; }

        // Navigation properties
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<ClinicalRecommendation>? ClinicalRecommendations { get; set; }
    }
} 