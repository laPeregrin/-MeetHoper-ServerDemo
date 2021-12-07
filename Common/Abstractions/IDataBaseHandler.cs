using Common.Models.DTOs;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Abstractions
{
    public interface IDataBaseHandler<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<TEntity> GetEntityByIdAsync(TId id);
        Task<TEntity> GetEntityByActionAsync(Expression<Func<TEntity, bool>> func);
        Task<bool> IsEntityExistAsync(TId id);
        Task<bool> UpdateEntityAsync(TEntity entity);
        Task SaveEntityAsync(TEntity entity);
    }

    public interface IDataBaseUserHandler : IDataBaseHandler<User, Guid>
    {

    }

}
