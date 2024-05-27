using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Auth
{
    public class RegisterResponse
    {
        public int LietotajsID { get; set; }
        public required string Lietotajvards { get; set; }
    }
}
