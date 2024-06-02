using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IDialogsManager : IBaseManager<Dialogs>
    {
        Task<bool> StopDialogue(int dialogueId);
        Task<bool> StartDialogue(int receiverId);
    }
}
