using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.core.Entites;

namespace taskm.core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();
 
        Task AddAsync(T item);

        void Delete(T item);

        void Update(T item);

        Task<int> CompleteAsync();
    }
}
