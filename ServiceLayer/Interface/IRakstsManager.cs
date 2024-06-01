using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;

namespace ServiceLayer.Interface
{
    public interface IRakstsManager : IBaseManager<Raksts>
    {
        Task<RakstsDto> Get(int id);
        Task<List<RakstsDto>> GetAll(int? specialistsId = null);
        Task<RakstsCreateResponseDto> Create(RakstsCreateDto rating, int user_id);
        Task<bool> Delete(int raksts_id, int user_id, List<string> user_roles);
        Task<bool> Update(int id, int user_id, List<string> user_roles, RakstsUpdateDto updated_raksts);
    }
}
