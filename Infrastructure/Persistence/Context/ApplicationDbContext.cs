using Domain.User;
using Domain.UserData;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;


namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Users> Users { get; init; }
        public DbSet<UserDataAccess> UserDataAccess { get; init; }
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            this.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>().ToCollection("users");
            modelBuilder.Entity<UserDataAccess>().ToCollection("userdataaccess");
        }
    }
}
