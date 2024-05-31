using DomainLayer.dto;
using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IZinaManager : IBaseManager<Zina>
    {
        Task<List<ZinaDto>> GetZinas(int dialogsId);
        Task<bool> PostZina(int receiverId, string zina);
    }
}
    