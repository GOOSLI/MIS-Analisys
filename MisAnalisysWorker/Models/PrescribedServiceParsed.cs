using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisAnalisysWorker.Models
{
    public class PrescribedServiceParsed
    {
        [Key]
        public int PrescribedServiceId { get; set; }
        
        [ForeignKey("ServiceNavigation")]
        public int ServiceId { get; set; }
        
        [ForeignKey("AppointmentNavigation")]
        public int AppointmentId { get; set; }

        // Navigation properties
        public AvailableService? ServiceNavigation { get; set; }
        public Appointment? AppointmentNavigation { get; set; }
    }
} 