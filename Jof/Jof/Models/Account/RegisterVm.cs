using System.ComponentModel.DataAnnotations;

namespace Jof.Models.Account
{
    public class RegisterVm
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 karekter daxil edebilersiz")]
        [MaxLength(25, ErrorMessage = "Maxsimum 25 karater daxil ede bilersiz")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 karekter daxil edebilersiz")]
        [MaxLength(25, ErrorMessage = "Maxsimum 25 karater daxil ede bilersiz")]
        public string Surname { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Minimum 8 karekter daxil edebilersiz")]
        [MaxLength(25, ErrorMessage = "Maxsimum 25 karater daxil ede bilersiz")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Comfirm passwordu duzgun daxil edin")]
        public string ConfirmPassword { get; set; }
    }
}
