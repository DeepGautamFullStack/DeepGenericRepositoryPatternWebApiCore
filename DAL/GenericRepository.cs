using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DeepGenericRepositoryPatternWebApiCore.DAL
{
    public class GenericRepository<TEntity> :IGenericRepository<TEntity> where TEntity : class
    {
        internal RepositoryDbContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository( RepositoryDbContext _context)
        {
            this.context = _context;
            this.dbSet = context.Set<TEntity>();
        }

        public   IEnumerable<TEntity> GetEntity(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public  TEntity GetEntityById(object id)
        {
            return dbSet.Find(id);
        }

        public  void InsertEntity(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public  void DeleteEntity(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        private  void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public  void UpdateEntity(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

       
    }
}
