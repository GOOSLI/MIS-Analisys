using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisAnalisysWorker.Models
{
    public class ClinicalRecommendation
    {
        [Key]
        public int RecommendationId { get; set; }
        
        [ForeignKey("DiagnosisNavigation")]
        public int DiagnosisId { get; set; }
        
        public string? Description { get; set; }
        public string? ApplicableConditions { get; set; }

        // Navigation properties
        public Diagnosis? DiagnosisNavigation { get; set; }
    }
} 