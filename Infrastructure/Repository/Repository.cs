using Application.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? fillter = null, string? includeProperty = null)         /// Mohammad 2026/1/15
        {
            IQueryable<T> query = _dbSet;
            if (fillter != null)
            {
                query = query.Where(fillter);
            }
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> fillter, string? includeProperty = null, bool tracking = false)
        {
            IQueryable<T> query;

            if (tracking)
            {
                query = _dbSet;
            }
            else
            {
                query = _dbSet.AsNoTracking();
            }
            query = query.Where(fillter);

            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }
        public IQueryable<T> Query( Expression<Func<T, bool>>? filter = null, string? include = null, bool tracking = false)
        {
            IQueryable<T> query = tracking
                ? _dbSet
                : _dbSet.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(include))
            {
                foreach (var prop in include.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop.Trim());
                }
            }

            return query;
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
