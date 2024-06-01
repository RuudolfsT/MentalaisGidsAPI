using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.Dto;

namespace ServiceLayer.Interface
{
    public interface ILietotajsRakstsKomentarsManager : IBaseManager<LietotajsRakstsKomentars>
    {
        Task<bool> CreateOrUpdate(RakstsKomentarsCreateDto komentars, int user_id, int raksts_id);
    }
}
