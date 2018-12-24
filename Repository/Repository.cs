using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiC.Repository
{
    public interface Repository<T,E>
    {
        IEnumerable<T> FindAll();
        Task<T> FindById(int id);
        Task<T> Edit(int id, E dto);
        Task<T> Add(E dto);
        Task<T> Remove(int id);
    }
}