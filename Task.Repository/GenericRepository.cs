using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.core.Entites;
using taskm.core.Repositories.Contract;
using taskm.Core.Entites;
using taskm.Repository.Data;
using taskm.Repository.Dtos;

namespace taskm.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TaskDbContext _dbContext;

        public GenericRepository(TaskDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Tasks))
            {
                return  await _dbContext.Tasks.Where(T=>T.Id ==id).Include(T => T.TeamMember).FirstOrDefaultAsync() as T;
            }
           
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Tasks))
            {
                return (IReadOnlyList<T>)await _dbContext.Tasks.Include(T => T.TeamMember).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item);

        public void Update(T item)
        => _dbContext.Set<T>().Update(item);

        public void Delete(T item)
        => _dbContext.Set<T>().Remove(item);

        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

    }
}
