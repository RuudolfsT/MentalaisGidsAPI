using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace ServiceLayer
{
    public class SajutuNovertejums : BaseManager<SajutuNovertejums>, ISajutuNovertejumsManager
    {
        private readonly MentalaisGidsContext _context;

        public SajutuNovertejums(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        public Task<bool> Create(SajutuNovertejumsCreateDto rating, int user_id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int user_id)
        {
            throw new NotImplementedException();
        }

        public Task<SajutuNovertejumsDto> Get(int user_id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SajutuNovertejumsDto>> GetAll(int user_id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, int user_id, SajutuNovertejumsUpdateDto updated_raksts)
        {
            throw new NotImplementedException();
        }
    }
}
