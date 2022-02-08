using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeepGenericRepositoryPatternWebApiCore.DAL
{
  public  interface IGenericRepository<TEntity> where TEntity:class
    {
        public  IEnumerable<TEntity> GetEntity(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        public TEntity GetEntityById(object Id);

        public void InsertEntity(TEntity entityToInsert);

        public void DeleteEntity(object Id);

        public void UpdateEntity(TEntity enityToUpdate);
    }
}
