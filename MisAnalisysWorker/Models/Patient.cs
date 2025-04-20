using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisAnalisysWorker.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? ContactInfo { get; set; }

        // Navigation properties
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
} 