using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class NodarbibaCreateDto
    {
        [Required(ErrorMessage = "Speciālists ir nepieciešams")]
        public int SpecialistsID { get; set; }

        [Required(ErrorMessage = "Sākuma laiks ir nepieciešams")]
        [DataType(DataType.DateTime)]
        public DateTime Sakums { get; set; }

        [Required(ErrorMessage = "Beigu laiks ir nepieciešams")]
        [DataType(DataType.DateTime)]
        public DateTime Beigas { get; set; }

    }
}
