using System.ComponentModel.DataAnnotations;

namespace MentalaisGidsAPI.Domain.dto
{
    public class NodarbibaUpdateDto
    {
        [Required(ErrorMessage = "Sākuma laikam ir jābūt vērtībai")]
        [DataType(DataType.DateTime)]
        public DateTime? Sakums { get; set; }

        [Required(ErrorMessage = "Beigu laikam ir jābūt vērtībai")]
        [DataType(DataType.DateTime)]
        public DateTime? Beigas { get; set; }
        public object SpecialistsID { get; set; }
    }
}
