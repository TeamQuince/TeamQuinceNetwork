namespace SocialNetwork.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using SocialNetwork.Models;
    using SocialNetwork.Data.Migrations;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class SocialNetworkContext : IdentityDbContext<ApplicationUser>
    {
        public SocialNetworkContext()
            : base("SocialNetworkContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SocialNetworkContext, Configuration>());
        }

        public static SocialNetworkContext Create()
        {
            return new SocialNetworkContext();
        }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<GroupPost> GroupPosts { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<PostLike> PostLikes { get; set; }

        public IDbSet<CommentLike> CommentLikes { get; set; }

        public IDbSet<FriendshipRequest> FriendshipRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("LeftFriendId");
                    m.MapRightKey("RightFriendId");
                    m.ToTable("Friends");
                });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Members)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("GroupId");
                    m.ToTable("UsersGroups");
                });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.Owner)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}