using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;

namespace ServiceLayer.Interface
{
    public interface IRakstsManager : IBaseManager<Raksts>
    {
        Task<RakstsDto> Get(int id);
        Task<List<RakstsDto>> GetAll();
        Task<RakstsCreateResponseDto> Create(RakstsCreateDto rating, int user_id);
        Task<bool> Delete(int raksts_id, int user_id);
    }
}
