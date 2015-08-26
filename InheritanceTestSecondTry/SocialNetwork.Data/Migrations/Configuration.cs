namespace SocialNetwork.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SocialNetwork.Data.SocialNetworkContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "SocialNetwork.Data.SocialNetworkContext";
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SocialNetwork.Data.SocialNetworkContext context)
        {
        }
    }
}
