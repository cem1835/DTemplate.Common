using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DTemplate.Common.GenericRepo
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
     where TEntity : class, IEntity, new()

    {
        public DbContext _context;

        public EntityRepository(DBContextGeneric dbContextGeneric)
        {
            _context = dbContextGeneric.Context;
        }



        public ServiceResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            ServiceResult<TEntity> serviceResult = new ServiceResult<TEntity>(ServiceResultType.Unknown);

            try
            {
                serviceResult.Data = _context.Set<TEntity>().Where(x => x.IsDeleted == false).SingleOrDefault(filter);
                serviceResult.ServiceResultType = ServiceResultType.Success;
            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;
        }

        public ServiceResult<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            ServiceResult<List<TEntity>> serviceResult = new ServiceResult<List<TEntity>>(ServiceResultType.Unknown);

            try
            {
                serviceResult.Data = filter == null
                    ? _context.Set<TEntity>().Where(w => !w.IsDeleted).ToList()
                    : _context.Set<TEntity>().Where(w => !w.IsDeleted).Where(filter).ToList();

                serviceResult.ServiceResultType = ServiceResultType.Success;
            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;

        }

        public ServiceResult<TEntity> Add(TEntity entity)
        {
            ServiceResult<TEntity> serviceResult = new ServiceResult<TEntity>(ServiceResultType.Unknown);

            try
            {
                entity.CreatedAt = DateTime.Now;

                serviceResult.Data = entity;

                EntityEntry<TEntity> addedEntity = _context.Set<TEntity>().Add(entity);
                addedEntity.State = EntityState.Added;

                _context.SaveChanges();

                serviceResult.ServiceResultType = ServiceResultType.Success;
            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;

        }

        public ServiceResult<TEntity> Update(TEntity entity)
        {
            ServiceResult<TEntity> serviceResult = new ServiceResult<TEntity>(ServiceResultType.Unknown);

            try
            {
                entity.ModifiedAt = DateTime.Now;

                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;

                _context.SaveChanges();
                serviceResult.Data = entity;


                serviceResult.ServiceResultType = ServiceResultType.Success;
            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;
        }

        public ServiceResult<TEntity> Delete(TEntity entity)
        {
            ServiceResult<TEntity> serviceResult = new ServiceResult<TEntity>(ServiceResultType.Unknown);

            try
            {
                entity.IsDeleted = true;
                entity.ModifiedAt = DateTime.Now;

                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Modified;

                _context.SaveChanges();
                serviceResult.Data = entity;

                serviceResult.ServiceResultType = ServiceResultType.Success;
            }
            catch (Exception exception)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
                serviceResult.Message = exception.Message;
            }

            return serviceResult;
        }

 
    }
}
