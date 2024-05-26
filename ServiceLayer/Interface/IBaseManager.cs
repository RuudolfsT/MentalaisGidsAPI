using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IBaseManager<T>
    {
        Task<T> FindById(int id);
    }
}
