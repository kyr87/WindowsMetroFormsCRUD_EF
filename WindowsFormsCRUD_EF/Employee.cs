using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsCRUD_EF
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
