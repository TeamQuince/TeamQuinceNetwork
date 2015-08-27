namespace SocialNetwork.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using SocialNetwork.Models;
    using SocialNetwork.Data.Migrations;

    public class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext()
            : base("name=SocialNetworkContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SocialNetworkContext, Configuration>());
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<PostLike> PostLikes { get; set; }

        public IDbSet<CommentLike> CommentLikes { get; set; }

        public IDbSet<Wall> Walls { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Posts)
        //        .WithRequired(p => p.Author)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Comments)
        //        .WithRequired(c => c.Author)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Groups)
        //        .WithMany(g => g.Members)
        //        .Map(m =>
        //        {
        //            m.MapLeftKey("UserId");
        //            m.MapRightKey("GroupId");
        //            m.ToTable("UsersGroups");
        //        });

        //    modelBuilder.Entity<User>()
        //        .HasRequired(u => u.Wall);

        //    modelBuilder.Entity<Group>()
        //        .HasRequired(u => u.Wall);

        //    modelBuilder.Entity<Post>()
        //        .HasMany(p => p.Likes)
        //        .WithRequired(l => l.Post)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Comment>()
        //        .HasMany(p => p.Likes)
        //        .WithRequired(l => l.Comment)
        //        .WillCascadeOnDelete(false);
        //}
    }
}