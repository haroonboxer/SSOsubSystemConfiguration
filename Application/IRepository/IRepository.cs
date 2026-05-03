using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IRepository<T> where T : class 
    {

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? fillter =null ,string? includeProperty =null );
        Task<IEnumerable<T>> GetAsync( Expression<Func<T, bool>> fillter, string? includeProperty = null,bool tracking = false );
        IQueryable<T> Query(Expression<Func<T,bool>>? filter = null, string? include = null,bool tracking = false);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
    
    
}
