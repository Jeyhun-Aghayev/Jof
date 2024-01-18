using Jof.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Jof.Areas.Manage.Models
{
    public class UpdateFruitVm:BaseAudiTableEntity
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 herf daxil etmelisiz")]
        [MaxLength(20, ErrorMessage = "Maxsimum 20 herf daxil ede bilersiniz")]
        public string Title { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 herf daxil etmelisiz")]
        [MaxLength(20, ErrorMessage = "Maxsimum 20 herf daxil ede bilersiniz")]
        public string SubTitle { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
