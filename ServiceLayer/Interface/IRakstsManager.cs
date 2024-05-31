using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;

namespace ServiceLayer.Interface
{
    public interface IRakstsManager : IBaseManager<Raksts>
    {
        Task<RakstsDto> Get(int id);
        Task<List<RakstsDto>> GetAll();
        Task<RakstsRateResultDto> Rate(RakstsRateDto rating, int user_id, int id);
    }
}
