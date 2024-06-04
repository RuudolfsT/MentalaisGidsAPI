using DomainLayer.ValidationAttributes;
using MentalaisGidsAPI.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DomainLayer.Dto
{
    public class TestsCreateDto
    {
        [Required(ErrorMessage = "Apraksts ir nepieciešams.")]
        public string Apraksts { get; set; }
        [MinCollectionSize(1, "Jābūt norādīt vismaz vienam jautājumam.")]
        public virtual ICollection<Jautajums> Jautajums { get; set; } = new List<Jautajums>();
    }
}
