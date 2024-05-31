using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IDialogsManager : IBaseManager<Dialogs>
    {
        Task<bool> StopDialogue(int dialogueId);
    }
}
