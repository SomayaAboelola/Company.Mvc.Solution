using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer.Entities
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department is Required")]
        public string Code { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public ICollection<Employee> Employees { get; set; }= new HashSet<Employee>();  
    }
}
