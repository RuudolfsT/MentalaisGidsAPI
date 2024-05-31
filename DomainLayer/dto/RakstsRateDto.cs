using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    // DataAnnotations:
    // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-8.0
    public class RakstsRateDto
    {
        [Required]
        [Range(0, 10, ErrorMessage = "{0} jābūt starp {2} un {1} simboliem garam.")]
        public byte Balles { get; set; }
    }
}
