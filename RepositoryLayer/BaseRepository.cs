using RepositoryLayer.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MentalaisGidsAPI.Models;

namespace RepositoryLayer
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MentalaisGidsContext _context;
        public BaseRepository()
        {

        }

        public BaseRepository(MentalaisGidsContext context)
        {
            _context = context;
        }

        public async Task<T> FindById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }
    }
}
