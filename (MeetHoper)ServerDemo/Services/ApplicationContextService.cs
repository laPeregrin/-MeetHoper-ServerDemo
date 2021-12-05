using _MeetHoper_ServerDemo.EF.Contexts;
using Common.Abstractions;
using Common.Exceptions;
using Common.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace _MeetHoper_ServerDemo.Services
{
    sealed class ApplicationContextService : IDataBaseUserHandler
    {

        private readonly ApplicationDbContext _dbContext;

        public ApplicationContextService(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<User> GetEntityByActionAsync(Expression<Func<User, bool>> expression) =>
            await _dbContext.Users.FirstOrDefaultAsync(expression);

        public async Task<User> GetEntityByIdAsync(Guid id) =>
            await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<bool> UpdateEntityAsync(User entity)
        {
            try
            {
             _dbContext.Users.Update(entity);
             await _dbContext.SaveChangesAsync();
             return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task SaveEntityAsync(User entity)
        {
            try
            {
                using (var transScope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10), TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _dbContext.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    transScope.Complete();
                }
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message);
            }
        }
    }
}
