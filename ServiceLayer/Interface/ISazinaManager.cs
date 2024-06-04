using DomainLayer.dto;
using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface ISazinaManager : IBaseManager<Dialogs>
    {
        Task<bool> StopDialogue(int dialogueId);
        Task<bool> StartDialogue(int receiverId);
        Task<List<ZinaDto>> GetZinas(int dialogsId);
        Task<bool> PostZina(int receiverId, string zina);
    }
}
