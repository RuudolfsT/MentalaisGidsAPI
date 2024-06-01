using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class SajutuNovertejumsUpdateDto
    {
        [Required(ErrorMessage = "Ieraksta ID ir obligāts.")]
        public int IerakstsID { get; set; }
        [Required(ErrorMessage = "Novērtējumam jābūt vērtībai.")]
        [Range(1, 10, ErrorMessage = "SajutuNovertejums jābūt starp {1} un {2}.")]
        public byte SajutuNovertejums { get; set; }
        [StringLength(32767, MinimumLength = 1, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string? Saturs { get; set; }
    }

}
