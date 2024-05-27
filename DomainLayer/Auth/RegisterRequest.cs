using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Auth
{
    public class RegisterRequest
    {
        public required string Vards { get; set; }
        public required string Uzvards { get; set; }
        public int? Dzimums { get; set; }
        public required string Lietotajvards { get; set; }
        public required byte[] Parole { get; set; }
        public required string Epasts { get; set; }
    }
}
