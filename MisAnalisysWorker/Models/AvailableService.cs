using System.ComponentModel.DataAnnotations;

namespace MisAnalisysWorker.Models
{
    public class AvailableService
    {
        [Key]
        public int ServiceId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }

        // Navigation properties
        public ICollection<PrescribedServiceParsed>? PrescribedServicesParsed { get; set; }
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
} 