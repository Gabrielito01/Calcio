using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Calcio.Web.Data.Entities
{
    public class GroupDetailEntity
    {
        public int Id { get; set; }

        public TeamEntity Team { get; set; }

        [Display(Name = "Matches Played")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "Partite Vinte")]
        public int MatchesWon { get; set; }

        [Display(Name = "Partite Pareggiate")]
        public int MatchesTied { get; set; }

        [Display(Name = "Partite Perse")]
        public int MatchesLost { get; set; }

        public int Points => MatchesWon * 3 + MatchesTied;

        [Display(Name = "Goal Fatti")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goal Subiti")]
        public int GoalsAgainst { get; set; }
        [Display(Name = "Differenza Goal")]
        public int GoalDifference => GoalsFor - GoalsAgainst;

        public GroupEntity Group { get; set; }

    }
}
