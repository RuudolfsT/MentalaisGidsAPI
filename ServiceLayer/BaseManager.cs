using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class BaseManager<T> : IBaseManager<T>
        where T : class
    {
        private readonly MentalaisGidsContext _context;

        public BaseManager(MentalaisGidsContext context)
        {
            _context = context;
        }

        public async Task<T> FindById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> SaveOrUpdate(T entity) // varbūt nāksies pielabot
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                var key = _context.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties?.FirstOrDefault();
                var keyValue = key?.PropertyInfo?.GetValue(entity, null);
                var exists = set.Find(keyValue);

                if (exists != null)
                {
                    _context.Entry(exists).CurrentValues.SetValues(entity);
                }
                else
                {
                    set.Add(entity);
                }
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
