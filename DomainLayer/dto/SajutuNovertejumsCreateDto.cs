using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class SajutuNovertejumsCreateDto
    {
        [Required(ErrorMessage = "Novērtējumam jābūt vērtībai.")]
        [Range(1, 10, ErrorMessage = "SajutuNovertejums jābūt starp {1} un {2}.")]
        public byte SajutuNovertejums { get; set; }
        [StringLength(32767, MinimumLength = 1, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public string? Saturs { get; set; }
        [Required(ErrorMessage = "Datumam jābūt vērtībai.")]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Datumam jābūt pagātnē.")]
        public DateTime DatumsUnLaiks { get; set; }

        protected class PastDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object? value)
            {
                if (value is DateTime dateValue)
                {
                    return dateValue <= DateTime.Now;
                }
                return false;
            }
        }
    }

    
}
