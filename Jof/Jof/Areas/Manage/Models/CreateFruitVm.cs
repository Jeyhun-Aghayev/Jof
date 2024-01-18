using System.ComponentModel.DataAnnotations;

namespace Jof.Areas.Manage.Models
{
    public class CreateFruitVm
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 herf daxil etmelisiz")]
        [MaxLength(20, ErrorMessage = "Maxsimum 20 herf daxil ede bilersiniz")]
        public string Title { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 herf daxil etmelisiz")]
        [MaxLength(20, ErrorMessage = "Maxsimum 20 herf daxil ede bilersiniz")]
        public string SubTitle { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
