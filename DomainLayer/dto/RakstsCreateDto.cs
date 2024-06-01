using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class RakstsCreateDto
    {
        [Required(ErrorMessage = "Virsraksts ir nepieciešams.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string Virsraksts { get; set; }

        [Required(ErrorMessage = "Saturs ir nepieciešams.")]
        [StringLength(32767, MinimumLength = 1, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string Saturs { get; set; }
    }
}
