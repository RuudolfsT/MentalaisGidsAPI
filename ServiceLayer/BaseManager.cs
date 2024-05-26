using DomainLayer;
using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class BaseManager<T> : IBaseManager<T> where T : class
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
    }
}
