using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisAnalisysWorker.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        
        [ForeignKey("EmployeeTypeNavigation")]
        public int EmployeeType { get; set; }
        
        [ForeignKey("DepartmentNavigation")]
        public int? DepartmentId { get; set; }

        // Navigation properties
        public EmployeeType? EmployeeTypeNavigation { get; set; }
        public Department? DepartmentNavigation { get; set; }
        public ICollection<Appointment>? DoctorAppointments { get; set; }
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
} 