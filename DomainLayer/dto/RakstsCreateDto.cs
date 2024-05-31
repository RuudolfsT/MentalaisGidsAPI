using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class RakstsCreateDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string Virsraksts { get; set; }
        [Required]
        [StringLength(32767, MinimumLength = 1, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string Saturs { get; set; }
    }
}
