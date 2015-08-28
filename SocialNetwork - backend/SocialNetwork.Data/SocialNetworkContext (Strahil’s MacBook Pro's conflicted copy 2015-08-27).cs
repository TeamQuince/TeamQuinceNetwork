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

        public IDbSet<Post> Postings { get; set; }

        public IDbSet<UserPost> UserPosts { get; set; }

        public IDbSet<GroupPost> GroupPosts { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<UserPostComment> UserPostComments { get; set; }

        public IDbSet<GroupPostComment> GroupPostComments { get; set; }

        public IDbSet<PostLike> Likes { get; set; }

        public IDbSet<UserPostLike> UserPostLikes { get; set; }

        public IDbSet<GroupPostLike> GroupPostLikes { get; set; }

        public IDbSet<UserPostCommentLike> UserPostCommentLikes { get; set; }

        public IDbSet<GroupPostCommentLike> GroupPostCommentLikes { get; set; }

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

            modelBuilder.Entity<UserPost>()
                .HasMany(p => p.Likes)
                .WithRequired(l => l.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupPost>()
                .HasMany(p => p.Likes)
                .WithRequired(l => l.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserPostComment>()
                .HasMany(p => p.Likes)
                .WithRequired(l => l.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupPostComment>()
                .HasMany(p => p.Likes)
                .WithRequired(l => l.Comment)
                .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}