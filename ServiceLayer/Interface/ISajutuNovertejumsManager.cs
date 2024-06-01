using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;

namespace ServiceLayer.Interface
{
    public interface ISajutuNovertejumsManager : IBaseManager<SajutuNovertejums>
    {
        // Jāzin user_id jo gan lietotajs, gan lietotaja specialists var redzet novertejumus
        // Also, Lietotaja tabulā ir kolonna "Anonimitate", bet man liekas ka seit tas nav aktuāli
        // pēc darījumprasībām (mosk jaslepj vards/uzvards atgrieztajiem datiem tho)
        Task<SajutuNovertejumsDto> Get(int user_id);
        Task<List<SajutuNovertejumsDto>> GetAll(int user_id);
        Task<bool> Create(SajutuNovertejumsCreateDto rating, int user_id);
        Task<bool> Delete(int id, int user_id);
        Task<bool> Update(int id, int user_id, SajutuNovertejumsUpdateDto updated_raksts);
    }
}
