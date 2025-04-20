using System.ComponentModel.DataAnnotations;

namespace MisAnalisysWorker.Models
{
    public class EmployeeType
    {
        [Key]
        public int EmployeeTypeId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Employee>? Employees { get; set; }
    }
} 