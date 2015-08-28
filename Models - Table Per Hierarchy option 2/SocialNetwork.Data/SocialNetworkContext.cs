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
            //modelBuilder.Entity<UserWallPost>()
            //    .HasRequired(up => up.WallOwner)
            //    .WithMany(a => a.UserWallPosts)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<UserWallPostComment>()
            //    .HasRequired(c => c.UserPost)
            //    .WithMany(up => up.Comments)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<GroupWallPost>()
            //    .HasRequired(up => up.Author)
            //    .WithMany(a => a.GroupWallPosts)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<GroupWallPostComment>()
            //    .HasRequired(c => c.GroupPost)
            //    .WithMany(gp => gp.Comments)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Like>()
                .HasRequired(c => c.Posting)
                .WithMany(gp => gp.Likes)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Group>().
            //  HasMany(gr => gr.Members).
            //  WithMany(u => u.Groups).
            //  Map(
            //   m =>
            //   {
            //       m.MapLeftKey("UserId");
            //       m.MapRightKey("GroupId");
            //       m.ToTable("UserGroups");
            //   });

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Groups)
            //    .WithRequired()
            //    .HasForeignKey(u => u.Id);

            modelBuilder.Entity<Posting>()
                .Map<UserWallPost>(m => m.Requires("Type").HasValue("UserWallPost"))
                .Map<UserWallPostComment>(m => m.Requires("Type").HasValue("UserWallPostComment"))
                .Map<GroupWallPost>(m => m.Requires("Type").HasValue("GroupWallPost"))
                .Map<GroupWallPostComment>(m => m.Requires("Type").HasValue("GroupWallPostComment"));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}