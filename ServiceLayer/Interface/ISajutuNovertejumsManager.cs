using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;

namespace ServiceLayer.Interface
{
    public interface ISajutuNovertejumsManager : IBaseManager<SajutuNovertejums>
    {
        // Lietotaja tabulā ir kolonna "Anonimitate", bet man liekas ka seit tas nav aktuāli
        // pēc darījumprasībām (mosk jaslepj vards/uzvards atgrieztajiem datiem tho)
        Task<SajutuNovertejumsDto> Get(int id, int user_id, List<string> user_roles);
        Task<List<SajutuNovertejumsDto>> GetAll(int requestingUserId, int ownerUserId, List<string> user_roles);
        Task<bool> Create(SajutuNovertejumsCreateDto novertejums, int user_id);
        Task<bool> Delete(int id, int user_id);
        Task<bool> Update(int id, int user_id, SajutuNovertejumsUpdateDto updated_novertejums);
    }
}
