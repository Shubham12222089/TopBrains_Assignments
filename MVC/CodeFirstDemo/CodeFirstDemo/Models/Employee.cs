using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstDemo.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [DisplayName("Employee Name")]
        [Required(ErrorMessage ="Employee Name is Required")]
        [StringLength(100, MinimumLength =3)]
        public string EmpName { get; set; }

        [Required(ErrorMessage ="Employee Address is Required")]
        [StringLength(300)]
        public string Address { get; set; }
        [Required(ErrorMessage ="Salary is Required")]
        [Range(3000,1000000, ErrorMessage ="salary must be between 3000 and 1000000")]
        public double Salary { get; set; }

        [Required(ErrorMessage ="Please Enter your Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage ="Please Enter Correct Address")]
        public string Email { get; set; }
    }
}
