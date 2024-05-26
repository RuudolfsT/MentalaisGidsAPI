using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Models;

namespace ServiceLayer.Interface
{
    internal interface ILietotajsManager
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<Lietotajs> GetAll();
        Lietotajs GetById(int id);
    }
}
