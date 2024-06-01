using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface ILomaManager : IBaseManager<Loma>
    {
        Task<bool> RoleExists(string id);
    }
}
