using System.ComponentModel.DataAnnotations;

namespace Aramex.Models
{
    public class VMUser
    {
        //public int UserId { get; set; }

        //public string UserName { get; set; } = null!;
        //[Required]
        public string Email { get; set; } = null!;
        //[Required]
        public string Password { get; set; } = null!;
    }
}
