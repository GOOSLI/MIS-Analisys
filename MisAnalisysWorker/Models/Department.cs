using System.ComponentModel.DataAnnotations;

namespace MisAnalisysWorker.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Employee>? Employees { get; set; }
    }
} 