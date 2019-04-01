using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PostService.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added).ToList();
            AddedEntities.ForEach(e =>
            {
                e.Entity.CreatedAt = DateTime.Now;
            });

            var ModifiedEntities = ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified).ToList();
            ModifiedEntities.ForEach(e =>
            {
                e.Entity.ModifiedAt = DateTime.Now;
            });

            return base.SaveChanges();
        }
    }
}
