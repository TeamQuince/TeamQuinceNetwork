namespace TQSN.Data.Migrations
{
    using System.Data.Entity.Migrations;

    class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
         public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = "TQSN.Data.ApplicationDbContext";
        }
         protected override void Seed(ApplicationDbContext context)
         {
            
         }
    }
}
