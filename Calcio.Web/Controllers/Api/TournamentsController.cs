﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Calcio.Web.Data;
using Calcio.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calcio.Web.Helpers;

namespace Soccer.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        public TournamentsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            List<TournamentEntity> tournaments = await _context.Tournaments
            .Include(t => t.Groups)
            .ThenInclude(g => g.GroupDetails)
            .ThenInclude(gd => gd.Team)
            .Include(t => t.Groups)
            .ThenInclude(g => g.Matches)
            .ThenInclude(m => m.Local)
            .Include(t => t.Groups)
            .ThenInclude(g => g.Matches)
            .ThenInclude(m => m.Visitor)
            .ToListAsync();
            return Ok(_converterHelper.ToTournamentResponse(tournaments));

        }
    }
}
