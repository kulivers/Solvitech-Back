using Microsoft.EntityFrameworkCore;
using Solvintech.Entities;

namespace Solvintech.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();
        }
    }
}