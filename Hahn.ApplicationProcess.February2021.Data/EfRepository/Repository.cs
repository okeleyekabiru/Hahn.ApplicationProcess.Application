using Hahn.ApplicationProcess.February2021.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.EfRepository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly HahnDbContext _context;

        public Repository(HahnDbContext context)
        {
            _context = context;
        }

        public T Get(long id) => _context.Set<T>().Find(id);

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Add(T entity) => _context.Entry<T>(entity).State = EntityState.Added;

        public void Remove(T entity) => _context.Entry<T>(entity).State = EntityState.Deleted;

        public void Update(T entity) => _context.Entry<T>(entity).State = EntityState.Modified;

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Added;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public IQueryable<T> FindAsQueryable() => _context.Set<T>().AsQueryable<T>();

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsQueryable();
        }

    }

}
