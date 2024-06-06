using DALayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PLayer.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public double Salary { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }

        public int DepartmentId { get; set; }
    }
}
