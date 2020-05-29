using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Calcio.Web.Data.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public TournamentEntity Tournament { get; set; }
        public ICollection<GroupDetailEntity> GroupDetails { get; set; }
        public ICollection<MatchEntity> Matches { get; set; }

    }
}
