namespace CS.Data.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using CS.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<ChallengesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }
    }
}
