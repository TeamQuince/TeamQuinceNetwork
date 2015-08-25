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

        public IDbSet<UserPost> UserPosts { get; set; }

        public IDbSet<GroupPost> GroupPosts { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<UserPostComment> UserPostComments { get; set; }

        public IDbSet<GroupPostComment> GroupPostComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPost>()
                .HasRequired(up => up.Author)
                .WithMany(a => a.UserPosts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupPost>()
                .HasRequired(up => up.Author)
                .WithMany(a => a.GroupPosts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserPostComment>()
                .HasRequired(c => c.UserPost)
                .WithMany(up => up.Comments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupPostComment>()
                .HasRequired(c => c.GroupPost)
                .WithMany(up => up.Comments)
                .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}