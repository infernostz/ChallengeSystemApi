namespace CS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using System.Web.Http.Cors;

    using CS.Common.Models.InputModels;
    using CS.Common.Models.ViewModels;

    using CS.Data;
    using CS.Data.Repositories;
    using CS.Data.Contracts.Repositories;

    using CS.Models;
    

    [EnableCors("*", "*", "*")]
    public class ChallengesController : ApiController
    {
        private readonly IChallengeRepository challenges;

        public ChallengesController() : this(new ChallengeRepository(new ChallengesDbContext()))
        {
        }

        public ChallengesController(IChallengeRepository challengesRepository)
        {
            this.challenges = challengesRepository;
        }

        [HttpGet]
        [Route("api/challenges")]
        public IHttpActionResult GetCurrent()
        {
            return this.Ok(this.challenges.GetCurrentAvailableByModel());
        }

        [HttpGet]
        public IHttpActionResult GetNextAvailableChallengeDate()
        {
            return this.Ok(challenges.GetNextAvailableChallengeDate());
        }

        [HttpGet]
        [Route("api/challenges/completed")]
        public IHttpActionResult GetCompletedChallenges()
        {
            return this.Ok(this.challenges.GetCompletedChallengesByModel());
        }

        [HttpGet]
        [Route("api/challenges/all")]
        public IHttpActionResult GetAll()
        {
            return this.Ok(this.challenges.GetAllByModel());
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return this.Ok(this.challenges.GetByIdByModel(id));
        }

        [HttpPost]
        [Route("api/challenges/")]
        public IHttpActionResult Post([FromBody]ChallengeInputModel challenge)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.challenges.Add(challenge);
            this.challenges.SaveChanges();

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult CompleteChallenge([FromUri]int id, [FromBody]string answer)
        {
            if (answer == null || String.IsNullOrWhiteSpace(answer))
            {
                return this.BadRequest();
            }

            var item = this.challenges.GetById(id);
            item.CompletedAt = DateTime.Now;
            item.IsCompleted = true;
            item.Answer = answer;
            this.challenges.Update(item);
            this.challenges.SaveChanges();

            return this.Ok();
        }

        //[HttpPut]
        //public IHttpActionResult Put(int id, Challenge challenge)
        //{
        //    var item = this.challenges.GetById(id);
        //    item = challenge;

        //    this.challenges.Update(item);
        //    this.challenges.SaveChanges();

        //    return this.Ok();
        //}

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            this.challenges.DeleteById(id);
            this.challenges.SaveChanges();

            return this.Ok();
        }
    }
}