using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.interfaces
{
    public interface IRepository<T> where T : class
    {
       /// <summary>
       /// Get entity by id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        T Get(long id);

        /// <summary>
        /// Get all entity
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get list of entities that matches a specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

    /// <summary>
    /// Update entity 
    /// </summary>
    /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Add set of a entity
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// return an IQueryable for extra logic
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
        /// <summary>
        /// Remove set of enitity
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
