using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class RakstsUpdateDto
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string? Virsraksts { get; set; }
        [StringLength(32767, MinimumLength = 1, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string? Saturs { get; set; }
    }
}
