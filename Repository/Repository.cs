using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiC.Persistence
{
    public interface Repository<T,E>
    {
        IEnumerable<T> FindAll();
        Task<T> FindById(long id);
        Task<T> Edit(long id, E dto);
        Task<T> Add(E dto);
        Task<T> Remove(long id);
    }
}