namespace TQSN.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
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
        public IDbSet<FriendshipRequest> FriendshipRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
              modelBuilder.Entity<ApplicationUser>()
                  .HasOptional(g => g.Friends)
                  .WithOptionalDependent()
                  .WillCascadeOnDelete(false);

          //    modelBuilder.Entity<ApplicationUser>()
          //        .HasKey(t => t.WallId);

          //  modelBuilder.Entity<ApplicationUser>().HasKey(e => e.WallId);
          //  modelBuilder.Entity<WallPosts>().HasOptional(u => u.ApplicationUser).WithRequired(w => w.Wall);

          //  modelBuilder.Entity<Group>().HasKey(e => e.WallId);
          //  modelBuilder.Entity<WallPosts>().HasOptional(u => u.Group).WithRequired(w => w.Wall);

          

            //modelBuilder.Entity<Wall>()
            //    .HasOptional(t => t.ApplicationUser);

         

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasRequired(u => u.Wall)
            //    .WithRequiredPrincipal(u => u.ApplicationUser);

            //modelBuilder.Entity<Wall>()
            // .HasOptional(t => t.Group);

            //modelBuilder.Entity<Group>()
            //    .HasRequired(u => u.Wall)
            //    .WithRequiredPrincipal(u => u.Group);


             

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(u => u.Posts)
        //        .WithRequired(p => p.Author)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(u => u.Comments)
        //        .WithRequired(c => c.Author)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Comment>()
        //        .HasMany(c => c.Likes)
        //        .WithRequired(l => l.Comment)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Post>()
        //        .HasMany(p => p.Likes)
        //        .WithRequired(l => l.Post)
        //        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
