using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using MentalaisGidsAPI.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using ServiceLayer.Manager;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace ServiceLayer
{
    public class SajutuNovertejumsManager : BaseManager<SajutuNovertejums>, ISajutuNovertejumsManager
    {
        private readonly MentalaisGidsContext _context;

        public SajutuNovertejumsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        /*
         * Darījumprasības:
         * CAL_USER_RATE_CREATE
         */
        public async Task<bool> Create(SajutuNovertejumsCreateDto novertejums, int user_id)
        {
            var existing_novertejums = await _context.SajutuNovertejums
                .Where(nov => nov.LietotajsID == user_id)
                .Where(nov => nov.DatumsUnLaiks.Date == novertejums.DatumsUnLaiks.Date)
                .FirstOrDefaultAsync();

            if (existing_novertejums != null)
            {
                return false;
            }

            await SaveOrUpdate(new SajutuNovertejums
            {
                LietotajsID = user_id,
                SajutuNovertejums1 = novertejums.SajutuNovertejums,
                Saturs = novertejums.Saturs,
                DatumsUnLaiks = novertejums.DatumsUnLaiks
            });

            return true;
        }

        /*
         * Darījumprasības:
         * CAL_USER_RATE_DELETE
         */
        public async Task<bool> Delete(int id, int user_id)
        {
            var novertejums = await FindById(id);
            if (novertejums != null)
            {
                if (novertejums.LietotajsID != user_id)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            await Delete(novertejums);
            return true;
        }

        /*
         * Darījumprasības:
         * CAL_USER_RATE_VIEW
         */
        public async Task<SajutuNovertejumsDto> Get(int id, int user_id, List<string> user_roles)
        {
            var novertejums = await FindById(id);
            if (novertejums == null)
            {
                return null;
            }

            // Can Get if user requesting is either the creator of the SajutuNovertejumi,
            // or if a Specialists wants to see the SajutuNovertejumi and he is a Specialists to the user
            if (novertejums.LietotajsID != user_id)
            {
                if (user_roles.Contains(RoleUtils.Specialists))
                {
                    if (!await IsSpecialistsToLietotajs(user_id, novertejums.LietotajsID))
                    {
                        return null;
                    }
                } else
                {
                    return null;
                }
            }


            return new SajutuNovertejumsDto
            {
                IerakstsID = novertejums.IerakstsID,
                SajutuNovertejums = novertejums.SajutuNovertejums1,
                Saturs = novertejums.Saturs,
                DatumsUnLaiks = novertejums.DatumsUnLaiks
            };

            return null;

        }

        /*
         * Darījumprasības:
         * Nav bet butu forsi, ka ir, tapec ir
         */
        public async Task<List<SajutuNovertejumsDto>> GetAll(int requestingUserId, int ownerUserId, List<string> requestingUser_roles)
        {
            // Can Get All if user requesting is either the creator of the SajutuNovertejumi,
            // or if a Specialists wants to see the SajutuNovertejumi and he is a Specialists to the user
            if (ownerUserId != requestingUserId)
            {
                if (requestingUser_roles.Contains(RoleUtils.Specialists))
                {
                    if (!await IsSpecialistsToLietotajs(requestingUserId, ownerUserId))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            var novertejumi = await _context.SajutuNovertejums
                .Where(novertejums => novertejums.LietotajsID == ownerUserId)
                .Select(novertejums => new SajutuNovertejumsDto
                {
                    IerakstsID = novertejums.IerakstsID,
                    SajutuNovertejums = novertejums.SajutuNovertejums1,
                    Saturs = novertejums.Saturs,
                    DatumsUnLaiks = novertejums.DatumsUnLaiks
                })
                .ToListAsync();

            return novertejumi;
        }

        /*
         * Darījumprasības:
         * CAL_USER_RATE_EDIT
         */
        public async Task<bool> Update(int id, int user_id, SajutuNovertejumsUpdateDto updated_novertejums)
        {
            var existing_novertejums = await FindById(id);

            if (existing_novertejums == null)
            {
                return false;
            }
            if (existing_novertejums.LietotajsID != user_id)
            {
                return false;
            }

            existing_novertejums.SajutuNovertejums1 = updated_novertejums.SajutuNovertejums;
            existing_novertejums.Saturs = updated_novertejums.Saturs;

            await SaveOrUpdate(existing_novertejums);

            return true;
        }

        public async Task<bool> IsSpecialistsToLietotajs(int specialists_id, int lietotajs_id)
        {
            var ok = await _context.LietotajsSpecialists
                .Where(liet_spec => liet_spec.LietotajsID == lietotajs_id)
                .Where(liet_spec => liet_spec.SpecialistsID == specialists_id)
                .Where(liet_spec => liet_spec.PieteiksanasStatuss == true)
                .Where(liet_spec => liet_spec.Blokets == false)
                .FirstOrDefaultAsync();

            return ok != null;
        }
    }
}
