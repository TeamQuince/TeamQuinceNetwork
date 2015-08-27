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

        public IDbSet<FriendshipRequest> FriendshipRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("LeftFriendId");
                    m.MapRightKey("RightFriendId");
                    m.ToTable("Friends");
                });
        }
    }
}