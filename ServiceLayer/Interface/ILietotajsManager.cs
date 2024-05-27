using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface ILietotajsManager : IBaseManager<Lietotajs>
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest model);
        IEnumerable<Lietotajs> GetAll();
        void Register(RegisterRequest model);
        void Delete();
    }
}
