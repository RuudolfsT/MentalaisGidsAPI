using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using MentalaisGidsAPI.Domain.Dto;

namespace ServiceLayer.Interface
{
    public interface INodarbibaManager : IBaseManager<Nodarbiba>
    {
        Task<NodarbibaDto> Get(int id);
        Task<List<NodarbibaDto>> GetAll(int? specialistsId = null);
        Task<bool> Create(NodarbibaCreateDto nodarbiba, int user_id);
        Task<bool> Delete(int id, int user_id);
        Task<bool> Update(int id, int user_id, NodarbibaUpdateDto updated_nodarbiba);
        Task<bool> ApplyToNodarbibaAsync(int nodarbibaId, int lietotajsId);
        Task<bool> CancelNodarbibaAsync(int nodarbibaId, int lietotajsId);
    }
}
