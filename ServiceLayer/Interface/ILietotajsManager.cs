using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface ILietotajsManager : IBaseManager<Lietotajs>
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest model);
        IEnumerable<Lietotajs> GetAll();
        RegisterResponse? Register(RegisterRequest model);
        Task<bool> UserExists(int id);
    }
}
