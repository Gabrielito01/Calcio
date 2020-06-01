using Microsoft.AspNetCore.Http;
using Calcio.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Calcio.Web.Models
{
    public class TournamentViewModel : TournamentEntity
    {
        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }
    }
}

