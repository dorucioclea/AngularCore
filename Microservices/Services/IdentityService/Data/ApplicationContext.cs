﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFriend> UserFriends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriend>().HasKey(uf => new {
                uf.UserId,
                uf.FriendId
            });
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

        private void ModifyEntitiesOnSave()
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
        }
    }
}
