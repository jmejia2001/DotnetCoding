using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;

namespace DotnetCoding.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task Add(T product)
        {
            await _dbContext.Set<T>().AddAsync(product);
        }
        public void Update(T product)
        {
            _dbContext.Update(product);
        }
        public void Delete(T product)
        {
            _dbContext.Remove(product);
        }

    }
}
