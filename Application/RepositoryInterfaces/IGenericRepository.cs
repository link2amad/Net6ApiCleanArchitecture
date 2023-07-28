#region Imports

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace Application.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);

        Task<T> Get(object id);

        Task<IEnumerable<T>> GetAll();

        IList<T> GetMany(Func<T, bool> where, params Expression<Func<T, object>>[] includes);

        bool Exists(Func<T, bool> where);

        T GetFirst(Func<T, bool> where, params Expression<Func<T, object>>[] includes);

        Task Add(T entity);

        Task AddRange(IList<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Delete(Func<T, bool> where);

        int Complete();
    }
}