using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisAnalisysWorker.Models
{
    public class ScheduledService
    {
        [Key]
        public int ScheduledServiceId { get; set; }
        
        [ForeignKey("ServiceNavigation")]
        public int ServiceId { get; set; }
        
        [ForeignKey("AppointmentNavigation")]
        public int? AppointmentId { get; set; }
        
        [ForeignKey("ScheduledForNavigation")]
        public int ScheduledForId { get; set; }
        
        [ForeignKey("ScheduledByNavigation")]
        public int ScheduledById { get; set; }
        
        public bool? Executed { get; set; }
        public DateTime PlannedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public AvailableService? ServiceNavigation { get; set; }
        public Appointment? AppointmentNavigation { get; set; }
        public Patient? ScheduledForNavigation { get; set; }
        public Employee? ScheduledByNavigation { get; set; }
    }
} 