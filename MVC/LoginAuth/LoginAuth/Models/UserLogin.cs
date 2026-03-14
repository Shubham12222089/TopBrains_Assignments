using System.ComponentModel.DataAnnotations;

namespace LoginAuth.Models
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter UserName")]
        [Display(Name ="Please Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Please Enter Password")]
        public string Passcode { get; set; }

        public int isActive { get; set; }
    }
}
