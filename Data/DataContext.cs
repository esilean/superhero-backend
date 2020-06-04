using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> Followings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Activity>(entity => entity.Property(m => m.Id).HasMaxLength(100));
            builder.Entity<User>(entity => entity.Property(m => m.Id).HasMaxLength(100));
            builder.Entity<UserActivity>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(100);
                entity.Property(m => m.ActivityId).HasMaxLength(100);
            });

            builder.Entity<Photo>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(100);
            });
            builder.Entity<Comment>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(100);
            });
            builder.Entity<UserFollowing>(entity =>
            {
                entity.Property(m => m.ObserverId).HasMaxLength(100);
                entity.Property(m => m.TargetId).HasMaxLength(100);
            });


            builder.Entity<UserActivity>(x => x.HasKey(ua =>
                new
                {
                    ua.UserId,
                    ua.ActivityId
                }));

            builder.Entity<UserActivity>()
                .HasOne(u => u.User)
                .WithMany(a => a.UserActivities)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserActivity>()
                        .HasOne(a => a.Activity)
                        .WithMany(u => u.UserActivities)
                        .HasForeignKey(a => a.ActivityId);

            builder.Entity<UserFollowing>(b =>
                    {
                        b.HasKey(k => new
                        {
                            k.ObserverId,
                            k.TargetId
                        });

                        b.HasOne(o => o.Observer)
                            .WithMany(f => f.Followings)
                            .HasForeignKey(o => o.ObserverId)
                            .OnDelete(DeleteBehavior.Restrict);

                        b.HasOne(o => o.Target)
                                    .WithMany(f => f.Followers)
                                    .HasForeignKey(o => o.TargetId)
                                    .OnDelete(DeleteBehavior.Restrict);
                    });

        }
    }
}
