namespace CS.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using CS.Common.Models.InputModels;
    using CS.Common.Models.ViewModels;

    using CS.Data.Contracts.Repositories;

    using CS.Models;

    public class ChallengeRepository : IChallengeRepository
    {
        private readonly ChallengesDbContext dbContext;

        public ChallengeRepository(ChallengesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Challenge> GetAll()
        {
            return this.dbContext.Challenges.ToList();
        }

        public IEnumerable<ChallengePartialViewModel> GetAllByModel()
        {
            return this.dbContext.Challenges
                .Select(x => new ChallengePartialViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    })
                .ToList();
        }

        public IEnumerable<Challenge> GetCurrentAvailable()
        {
            var currentDateTime = DateTime.Now;

            return this.dbContext.Challenges.Where(c => (currentDateTime >= c.StartDate) && !c.IsCompleted).ToList();
        }

        public IEnumerable<ChallengePartialViewModel> GetCurrentAvailableByModel()
        {
            var currentDateTime = DateTime.Now;

            return this.dbContext.Challenges
                    .Where(c => (currentDateTime >= c.StartDate) && !c.IsCompleted)
                    .Select(x => new ChallengePartialViewModel()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        })
                    .ToList();
        }

        public IEnumerable<Challenge> GetCompletedChallenges()
        {
            return this.dbContext.Challenges.Where(c => c.IsCompleted).ToList();
        }

        public IEnumerable<CompletedChallengeView> GetCompletedChallengesByModel()
        {
            return this.dbContext.Challenges
                    .Where(c => c.IsCompleted)
                    .Select(x => new CompletedChallengeView()
                        {
                            Name = x.Name,
                            Description = x.Description,
                            CompletedAt = x.CompletedAt,
                            Answer = x.Answer,
                            Reward = x.Reward
                        })
                    .ToList();
        }

        public Challenge GetById(int id)
        {
            return this.dbContext.Challenges.Find(id);
        }

        public ChallengeViewModel GetByIdByModel(int id)
        {
            var item = this.dbContext.Challenges.Find(id);

            return new ChallengeViewModel()
            {
                Name = item.Name,
                Description = item.Description,
                MinimumWordsRestriction = item.MinimumWordsRestriction,
                StartDate = item.StartDate,
                Reward = item.Reward,
                IsCompleted = item.IsCompleted
            };
        }

        public ChallengePartialViewModel GetByIdByModelPartial(int id)
        {
            var item = this.dbContext.Challenges.Find(id);

            return new ChallengePartialViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description
                        };
        }

        public DateTime GetNextAvailableChallengeDate()
        {
            var currentDateTime = DateTime.Now;

            return this.dbContext.Challenges
                    .Where(x => (x.StartDate >= currentDateTime) && !x.IsCompleted)
                    .OrderBy(x => x.StartDate)
                    .Select(x => x.StartDate)
                    .First();
        }

        public Challenge Add(ChallengeInputModel challenge)
        {
            var entry = new Challenge()
            {
                Name = challenge.Name,
                Description = challenge.Description,
                StartDate = challenge.StartDate,
                MinimumWordsRestriction = challenge.MinimumWordsRestriction,
                Reward = challenge.Reward
            };

            return this.dbContext.Challenges.Add(entry);
        }

        public Challenge Delete(Challenge challenge)
        {
            return this.dbContext.Challenges.Remove(challenge);
        }

        public Challenge DeleteById(int id)
        {
            var item = this.dbContext.Challenges.Find(id);

            return this.dbContext.Challenges.Remove(item);
        }

        public void Update(Challenge challenge)
        {
            this.dbContext.Entry(challenge).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }
    }
}
