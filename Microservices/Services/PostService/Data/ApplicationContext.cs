using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostService.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ModifyEntitiesOnSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ModifyEntitiesOnSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ModifyEntitiesOnSave() {
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
        }
    }
}
