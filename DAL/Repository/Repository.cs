using API_Assignment.DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_Assignment.DAL.Repository
{
    public abstract class Repository<T, Tcontext> : IRepository<T> where T : class where Tcontext : DbContext
    {
        protected readonly DbContext _dbContext;

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ICollection<T>> GetAllAsync()
        {
            IQueryable<T> result = this._dbContext.Set<T>();
            return await result.ToListAsync();
        }




        public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition)
        {
            IQueryable<T> result = this._dbContext.Set<T>();
            if (condition != null)
            {
                result = result.Where(condition);
            }

            return result;
        }




        public bool Add(T entity)
        {
            this._dbContext.Set<T>().Add(entity);
            return true;
        }

        public bool Update(T entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public bool Delete(T entity)
        {
            this._dbContext.Set<T>().Remove(entity);
            return true;
        }


        public void SaveChangesManaged()
        {
            this._dbContext.SaveChanges();
        }
    }


}
