using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface ITestsManager : IBaseManager<Tests>
    {
        Task<Tests> Get(int id);
        Task<List<Tests>> GetAll();
        Task<bool> Create(Tests test, int userId);
        Task<bool> Submit(int userId); // vajag parametru kkādu atbilde DTO
        Task<bool> Results(int userId);
        Task<bool> Delete(int userId);
    }
}
