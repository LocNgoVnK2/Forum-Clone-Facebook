using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class EXDbContext : DbContext
    {
        public EXDbContext(DbContextOptions<EXDbContext> options) : base(options)
        {
        }

        DbSet<Post> posts { get; set; }
        DbSet<User> Users { get; set; }

        DbSet<Like_Detail> Likes { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<FriendFollow> Followers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like_Detail>()
                .HasKey(l => new { l.PostID, l.UserID });
            //friend builder
              modelBuilder.Entity<FriendFollow>()
            .HasKey(ff => new { ff.UserID1, ff.UserID2 });

        modelBuilder.Entity<FriendFollow>()
            .HasOne(ff => ff.User1)
            .WithMany()
            .HasForeignKey(ff => ff.UserID1)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FriendFollow>()
            .HasOne(ff => ff.User2)
            .WithMany()
            .HasForeignKey(ff => ff.UserID2)
            .OnDelete(DeleteBehavior.Restrict);
        
        }

    }
}
