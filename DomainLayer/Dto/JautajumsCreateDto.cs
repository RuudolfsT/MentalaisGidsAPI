using MentalaisGidsAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DomainLayer.Dto
{
    public class JautajumsCreateDto
    {
        public string Saturs { get; set; }
        public ICollection<Atbilde> Atbilde { get; set; }
    }
}
