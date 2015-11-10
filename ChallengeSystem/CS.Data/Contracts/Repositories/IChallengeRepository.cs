namespace CS.Data.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;

    using CS.Common.Models.InputModels;
    using CS.Common.Models.ViewModels;

    using CS.Models;

    public interface IChallengeRepository
    {
        IEnumerable<Challenge> GetAll();

        IEnumerable<ChallengePartialViewModel> GetAllByModel();

        IEnumerable<Challenge> GetCurrentAvailable();

        IEnumerable<ChallengePartialViewModel> GetCurrentAvailableByModel();

        IEnumerable<Challenge> GetCompletedChallenges();

        IEnumerable<CompletedChallengeView> GetCompletedChallengesByModel();

        Challenge GetById(int id);

        ChallengeViewModel GetByIdByModel(int id);

        ChallengePartialViewModel GetByIdByModelPartial(int id);

        DateTime GetNextAvailableChallengeDate();

        Challenge Add(ChallengeInputModel challenge);

        Challenge Delete(Challenge challenge);

        Challenge DeleteById(int id);

        void Update(Challenge challenge);

        int SaveChanges();
    }
}