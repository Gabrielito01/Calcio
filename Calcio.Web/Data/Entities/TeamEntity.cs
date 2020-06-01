using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calcio.Web.Data.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage="the field {0} can not have more than {1} characters.")]
        [Required(ErrorMessage= "The field{0} is mandatory.")]
        public string Name{ get; set; }
        [Display(Name="Logo")]
        public string LogoPath{ get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}
