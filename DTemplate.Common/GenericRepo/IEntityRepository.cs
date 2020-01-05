using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DTemplate.Common.GenericRepo
{
    public interface IEntityRepository<TEntity>
     where TEntity : class, new()
    {
        ServiceResult<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        ServiceResult<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null);
        ServiceResult<TEntity> Add(TEntity entity);
        ServiceResult<TEntity> Update(TEntity entity);
        ServiceResult<TEntity> Delete(TEntity entity);
    }
}
