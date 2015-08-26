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
        public IDbSet<Group> Groups { get; set; }
        //public IDbSet<GroupWallPost> GroupWallPosts { get; set; }
        //public IDbSet<GroupWallPostComment> GroupWallPostComments { get; set; }
        public IDbSet<Like> Likes { get; set; }
        public IDbSet<Posting> Postings { get; set; }
        //public IDbSet<UserWallPost> UserWallPosts { get; set; }
        //public IDbSet<UserWallPostComment> UserPosts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWallPost>()
                .HasRequired(up => up.WallOwner)
                .WithMany(a => a.UserWallPosts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserWallPostComment>()
                .HasRequired(c => c.UserPost)
                .WithMany(up => up.Comments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupWallPost>()
                .HasRequired(up => up.Author)
                .WithMany(a => a.GroupWallPosts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupWallPostComment>()
                .HasRequired(c => c.GroupPost)
                .WithMany(up => up.Comments)
                .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}