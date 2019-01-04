using System;
using System.Linq;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFriends>()
                .HasKey( uf => new {uf.UserId, uf.FriendId} );
            modelBuilder.Entity<UserFriends>()
                .HasOne( uf => uf.User )
                .WithMany( u => u.UserFriends )
                .HasForeignKey( uf => uf.UserId );
            modelBuilder.Entity<UserFriends>()
                .HasOne( uf => uf.Friend )
                .WithMany( f => f.FriendUsers )
                .HasForeignKey( uf => uf.FriendId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Post>()
                .HasKey( p => p.Id );
            modelBuilder.Entity<Post>()
                .HasOne( p => p.Author )
                .WithMany( u => u.Posts )
                .HasForeignKey( p => p.AuthorId )
                .OnDelete( DeleteBehavior.Cascade );
            modelBuilder.Entity<Post>()
                .HasOne( p => p.WallOwner )
                .WithMany( u => u.WallPosts )
                .HasForeignKey( p => p.WallOwnerId )
                .OnDelete( DeleteBehavior.Cascade );

            modelBuilder.Entity<Comment>()
                .HasOne( c => c.User )
                .WithMany( u => u.Comments )
                .OnDelete( DeleteBehavior.Restrict );
        }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries<IEntityDate>().Where( e => e.State == EntityState.Added ).ToList();
            AddedEntities.ForEach( e =>
            {
                e.Entity.CreatedAt = DateTime.Now;
                if(e.Entity.GetType() == typeof(BaseEntity))
                {
                    ((BaseEntity) e.Entity).Id = Guid.NewGuid().ToString();
                }
            });

            var ModifiedEntities = ChangeTracker.Entries<IEntityDate>().Where( e => e.State == EntityState.Modified ).ToList();
            ModifiedEntities.ForEach( e =>
            {
                e.Entity.ModifiedAt = DateTime.Now;
            });

            return base.SaveChanges();
        }
    }
}