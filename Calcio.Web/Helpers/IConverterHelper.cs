using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calcio.Web.Data.Entities;
using Calcio.Web.Models;

namespace Calcio.Web.Helpers
{
    public interface IConverterHelper
    {
        TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity teamEntity);
    }
}
