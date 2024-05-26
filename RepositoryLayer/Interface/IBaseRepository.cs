using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBaseRepository<T>
    {
        Task<T> FindById(int id);
    }
}
