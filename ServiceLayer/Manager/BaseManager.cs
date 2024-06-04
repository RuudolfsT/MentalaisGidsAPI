using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer.Manager
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

        public async Task<T> FindByNameId(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> SaveOrUpdate(T entity) // varbūt nāksies pielabot
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                var keys = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
                var keyValues = keys.Select(k => k.PropertyInfo.GetValue(entity)).ToArray();
                var exists = set.Find(keyValues);

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

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
