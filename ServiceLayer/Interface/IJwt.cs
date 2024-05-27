using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IJwt
    {
        public string GenerateToken(Lietotajs lietotajs);
        public int? ValidateToken(string token);
    }
}
