using Common.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _MeetHoper_ServerDemo.EF.Contexts
{
    sealed class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

    }
}
