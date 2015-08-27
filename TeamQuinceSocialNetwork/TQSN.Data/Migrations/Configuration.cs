namespace TQSN.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Model;

    class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
         public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "TQSN.Data.ApplicationDbContext";
        }
         protected override void Seed(ApplicationDbContext context)
         {
            //// USERS
            //context.Users.AddOrUpdate(u => u.UserName, 
            //    new ApplicationUser()
            //    {
            //        UserName = "Pesho",
            //        Email = "Pesho@example.com"
            //    }
            //);  
         }
    }
}
