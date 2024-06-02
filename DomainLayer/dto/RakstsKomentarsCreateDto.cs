using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.Dto
{
    public class RakstsKomentarsCreateDto
    {
        [Required(ErrorMessage = "Saturs ir nepieciešams.")]
        [StringLength(32767, MinimumLength = 1, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string Saturs { get; set; }
    }
}
