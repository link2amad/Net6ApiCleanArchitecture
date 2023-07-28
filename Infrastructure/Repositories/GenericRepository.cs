#region Imports

using Application.RepositoryInterfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Get(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IList<T> GetMany(Func<T, bool> where, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).Where(where).ToList();
        }

        public T GetFirst(Func<T, bool> where, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).Where(where).FirstOrDefault();
        }

        public bool Exists(Func<T, bool> where)
        {
            return _context.Set<T>().Any(where);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task AddRange(IList<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Delete(object id)
        {
            var entityToDelete = _context.Set<T>().Find(id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
            }
        }

        public void Delete(Func<T, bool> where)
        {
            IQueryable<T> objects = _context.Set<T>().Where<T>(where).AsQueryable();
            _context.Set<T>().RemoveRange(objects);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}