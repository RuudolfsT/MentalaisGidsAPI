using MentalaisGidsAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ILomaManager
    {
        Task<Loma> GetLoma(int id);
    }
}
