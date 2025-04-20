using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisAnalisysWorker.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        
        [ForeignKey("DiagnosisNavigation")]
        public int DiagnosisId { get; set; }
        
        [ForeignKey("PatientNavigation")]
        public int PatientId { get; set; }
        
        [ForeignKey("DoctorNavigation")]
        public int DoctorId { get; set; }
        
        public string PrescriptionText { get; set; } = string.Empty;
        public string ConditionText { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public int? ProcessingStatus { get; set; }
        public int? ValidityIndex { get; set; }

        // Navigation properties
        public Diagnosis? DiagnosisNavigation { get; set; }
        public Patient? PatientNavigation { get; set; }
        public Employee? DoctorNavigation { get; set; }
        public ICollection<PrescribedServiceParsed>? PrescribedServicesParsed { get; set; }
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
} 