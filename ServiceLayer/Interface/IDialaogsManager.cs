using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IDialaogsManager : IBaseManager<Dialogs>
    {
        Task<bool> StopDialogue(int dialogueId);
    }
}
