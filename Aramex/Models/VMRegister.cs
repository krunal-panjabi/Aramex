using System.ComponentModel.DataAnnotations;

namespace Aramex.Models
{
    public class VMRegister
    {
        //public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast 1 lowercase,1 uppercase, 1 digit,1 special character and must be of 8 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
