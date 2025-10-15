using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnionArchitecture.Domain.Entities;
using System.Reflection;

namespace OnionArchitecture.infrastructure
{
    public class UserDBContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        //不需要为非根实体生成DBSet
        public DbSet<UserLoginHistory> userLoginHistories { get; set; }
        public UserDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
