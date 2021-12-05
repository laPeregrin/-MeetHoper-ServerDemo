using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Abstractions
{
    public interface IAuthenticationService<TEntity, TId> where TEntity : IEntity<TId>
    {
        IDataBaseHandler<TEntity, TId> DataHandler { get; set; }
        Task<bool> AuthenticateAsync();
    }
}
