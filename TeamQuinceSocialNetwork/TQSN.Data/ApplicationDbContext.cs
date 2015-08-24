namespace TQSN.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Model;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("QuinceSocialNetwork", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Like> Likes { get; set; }
        public IDbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Likes)
                .WithRequired(l => l.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithRequired(l => l.Post)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
