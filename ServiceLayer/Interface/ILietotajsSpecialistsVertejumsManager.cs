using DomainLayer;
using MentalaisGidsAPI.Domain;
namespace ServiceLayer.Interface
{
    public interface ILietotajsSpecialistsVertejumsManager : IBaseManager<LietotajsSpecialistsVertejums>
    {
        Task<Status> CreateOrUpdate(int rating, int userId, int specialistId);
        Task<List<LietotajsSpecialistsVertejums>> GetAllUserRates(int userId);
    }
}
