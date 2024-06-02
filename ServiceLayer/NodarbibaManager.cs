using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using MentalaisGidsAPI.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class NodarbibaManager : BaseManager<Nodarbiba>, INodarbibaManager
    {
        private readonly MentalaisGidsContext _context;

        public NodarbibaManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NodarbibaDto> Get(int id)
        {
            var nodarbiba = await _context.Nodarbiba
                .Include(n => n.Specialists)
                .Include(n => n.Lietotajs)
                .FirstOrDefaultAsync(n => n.NodarbibaID == id);

            if (nodarbiba != null)
            {
                var dto = new NodarbibaDto
                {
                    NodarbibaID = nodarbiba.NodarbibaID,
                    SpecialistsID = nodarbiba.SpecialistsID,
                    LietotajsID = nodarbiba.LietotajsID,
                    Sakums = nodarbiba.Sakums,
                    Beigas = nodarbiba.Beigas
                };
                return dto;
            }

            return null;
        }

        public async Task<List<NodarbibaDto>> GetAll(int? specialistsId = null)
        {
            var query = _context.Nodarbiba.AsQueryable();
            if (specialistsId != null)
            {
                query = query.Where(n => n.SpecialistsID == specialistsId.Value);
            }

            var dtos = await query
                .Include(n => n.Specialists)
                .Include(n => n.Lietotajs)
                .Select(n => new NodarbibaDto
                {
                    NodarbibaID = n.NodarbibaID,
                    SpecialistsID = n.SpecialistsID,
                    LietotajsID = n.LietotajsID,
                    Sakums = n.Sakums,
                    Beigas = n.Beigas
                })
                .ToListAsync();

            return dtos;
        }

        public async Task<bool> Create(NodarbibaCreateDto nodarbibaDto, int user_id)
        {
            var newNodarbiba = new Nodarbiba
            {
                SpecialistsID = nodarbibaDto.SpecialistsID,
                Sakums = nodarbibaDto.Sakums,
                Beigas = nodarbibaDto.Beigas
            };

            await SaveOrUpdate(newNodarbiba);
            return true;
        }

        public async Task<bool> Delete(int id, int user_id)
        {
            var nodarbiba = await FindById(id);

            if (nodarbiba == null || nodarbiba.SpecialistsID != user_id)
            {
                return false;
            }

            await Delete(nodarbiba);
            return true;
        }

        public async Task<bool> Update(int id, int user_id, NodarbibaUpdateDto updatedNodarbiba)
        {
            var nodarbiba = await FindById(id);

            if (nodarbiba == null || nodarbiba.SpecialistsID != user_id)
            {
                return false;
            }

            /*if (updatedNodarbiba.SpecialistsID)
            {
                nodarbiba.SpecialistsID = updatedNodarbiba.SpecialistsID.;
            }*/
            if (updatedNodarbiba.Sakums.HasValue)
            {
                nodarbiba.Sakums = updatedNodarbiba.Sakums.Value;
            }
            if (updatedNodarbiba.Beigas.HasValue)
            {
                nodarbiba.Beigas = updatedNodarbiba.Beigas.Value;
            }

            await SaveOrUpdate(nodarbiba);
            return true;
        }

        public async Task<bool> ApplyToNodarbibaAsync(int nodarbibaId, int lietotajsId)
        {
            var nodarbiba = await FindById(nodarbibaId);

            if (nodarbiba == null || nodarbiba.LietotajsID.HasValue)
            {
                return false;
            }

            nodarbiba.LietotajsID = lietotajsId;
            await SaveOrUpdate(nodarbiba);
            return true;
        }

        public async Task<bool> CancelNodarbibaAsync(int nodarbibaId, int lietotajsId)
        {
            var nodarbiba = await FindById(nodarbibaId);

            if (nodarbiba == null || nodarbiba.LietotajsID != lietotajsId)
            {
                return false;
            }

            nodarbiba.LietotajsID = null;
            await SaveOrUpdate(nodarbiba);
            return true;
        }
    }
}
