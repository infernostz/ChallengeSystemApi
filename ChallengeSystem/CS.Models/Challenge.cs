namespace CS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Challenge
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description {get; set;}

        public DateTime StartDate { get; set; }

        public int MinimumWordsRestriction { get; set; }

        public string Reward { get; set; }

        public string Answer { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}