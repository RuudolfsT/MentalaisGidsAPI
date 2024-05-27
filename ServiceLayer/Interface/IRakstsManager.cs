using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;

namespace ServiceLayer.Interface
{
    public interface IRakstsManager : IBaseManager<Raksts>
    {
        Task<RakstsDto> Get(int id);
        Task<List<RakstsDto>> GetAll();
    }
}
