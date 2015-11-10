namespace CS.Data
{
    using System.Data.Entity;

    using CS.Data.Migrations;
    using CS.Models;    

    public class ChallengesDbContext : DbContext
    {
        public ChallengesDbContext() : base("ChallengesDB")
        {
            Database.SetInitializer<ChallengesDbContext>(new MigrateDatabaseToLatestVersion<ChallengesDbContext, Configuration>());
        }

        public IDbSet<Challenge> Challenges { get; set; }
    }
}