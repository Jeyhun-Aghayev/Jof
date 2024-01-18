using System.ComponentModel.DataAnnotations;

namespace Jof.Models.Account
{
    public class LoginVm
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [MinLength(8,ErrorMessage ="Minimum 8 herf daxil edin")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
