using DomainLayer.dto;
using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IZinaManager : IBaseManager<Zina>
    {
        Task<ZinaDto> GetZina(int zinaId);
        Task<bool> PostZina(int userId, int dialogueId, string zina);
    }
}
    