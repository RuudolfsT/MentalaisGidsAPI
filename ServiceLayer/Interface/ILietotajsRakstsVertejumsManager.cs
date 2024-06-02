using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.Dto;

namespace ServiceLayer.Interface
{
    public interface ILietotajsRakstsVertejumsManager : IBaseManager<LietotajsRakstsVertejums>
    {
        Task<RakstsRateResponseDto> CreateOrUpdate(RakstsRateDto rating, int user_id, int id);
    }
}
