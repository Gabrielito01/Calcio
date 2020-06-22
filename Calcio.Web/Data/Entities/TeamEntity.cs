using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calcio.Web.Data.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        public string LogoPath { get; set; }

        [Display(Name = "Logo")]
        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
            ? "https://localhost:44343///images/noimage.png"
            : $"https://localhost:44343{LogoPath.Substring(1)}";

        public ICollection<UserEntity> Users { get; set; }
    }
}
