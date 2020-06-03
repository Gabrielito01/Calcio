using Calcio.Common.Enums;
using Calcio.Web.Data.Entities;
using Calcio.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calcio.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckTeamsAsync();
            await CheckTournamentsAsync();
            await CheckUserAsync("1010", "Gabriel", "Barreto", "gabrielbarreto421@gmail.com", "350 634 2747", "Via del circo", UserType.Admin);
            await CheckUserAsync("2020", "Andres", "Barreto", "andresbarreto.8@hotmail.com", "350 634 2747", "Via del circo", UserType.User);
            await CheckUserAsync("3030", "Gabriel", "Barreto", "barrgabbo@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("4040", "Gabriel", "Barreto", "barrgabbo@libero.it", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckPreditionsAsync();
        }

        private async Task CheckPreditionsAsync()
        {
            if (!_context.Predictions.Any())
            {
                foreach (var user in _context.Users)
                {
                    if (user.UserType == UserType.User)
                    {
                        AddPrediction(user);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        private void AddPrediction(UserEntity user)
        {
            var random = new Random();
            foreach (var match in _context.Matches)
            {
                _context.Predictions.Add(new PredictionEntity
                {
                    GoalsLocal = random.Next(0, 5),
                    GoalsVisitor = random.Next(0, 5),
                    Match = match,
                    User = user
                });
            }
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Team = _context.Teams.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }



        private async Task CheckTeamsAsync()
        {
            if (!_context.Teams.Any())
            {
                AddTeam("Ajax");
                AddTeam("Atalanta");
                AddTeam("Atletico");
                AddTeam("Barcelona");
                AddTeam("Bayern");
                AddTeam("Benfica");
                AddTeam("Borussia");
                AddTeam("Brugge");
                AddTeam("Chelsea");
                AddTeam("Dinamo");
                AddTeam("Galatasaray");
                AddTeam("Genk");
                AddTeam("Inter");
                AddTeam("Juventus");
                AddTeam("Leverkusen");
                AddTeam("Lipsia");
                AddTeam("Liverpool");
                AddTeam("Lokomotiv");
                AddTeam("Lylle");
                AddTeam("Lyon");
                AddTeam("Manchester City");
                AddTeam("Napoli");
                AddTeam("Olympiacos");
                AddTeam("Psg");
                AddTeam("Real Madrid"); 
                AddTeam("Salisburgo");
                AddTeam("Shaktar"); 
                AddTeam("Slavia"); 
                AddTeam("Stared"); 
                AddTeam("Tottenham");
                AddTeam("Valencia");
                AddTeam("Zenit");
                await _context.SaveChangesAsync();
            }
        }
        private void AddTeam(string name)
        {
            _context.Teams.Add(new TeamEntity { Name = name, LogoPath = $"~/images/Teams/{name}.png" });
        }




        private async Task CheckTournamentsAsync()
        {
            if (!_context.Tournaments.Any())
            {
                var startDate = DateTime.Today.AddMonths(2).ToUniversalTime();
                var endDate = DateTime.Today.AddMonths(3).ToUniversalTime();

                _ = _context.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    LogoPath = $"~/images/Tournaments/Champions.png",
                    Name = "Champions 2019/2020",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Psg") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Brugge") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brugge"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Psg"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brugge")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Psg")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brugge"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Psg")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Psg"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brugge")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(18),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brugge")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(18),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Psg")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brugge"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Real Madrid")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Psg"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Galatasaray")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Bayern") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Stared") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bayern"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Stared")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Stared"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bayern")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bayern")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Stared")
                                 },
                                    new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bayern"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Stared"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Stared"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bayern")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Olympiacos"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Stared")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bayern"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Tottenham")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "C",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Manchester City") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Manchester City")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Manchester City"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Manchester City"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta")
                                 },
                                  new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Manchester City")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Manchester city"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Shaktar"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atalanta")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dinamo"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Manchester City")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "D",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Juventus") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Atletico") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atletico"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Juventus")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Juventus"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atletico")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atletico"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Juventus"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv")
                                 },
                                  new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Juventus")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atletico")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Juventus"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atletico")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(25).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atletico"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lokomotiv")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(25).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Leverkusen"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Juventus")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "E",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Napoli") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Genk") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Genk")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Napoli"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Genk"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Napoli")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Napoli")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Genk"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool")
                                 },
                                  new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Napoli"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Genk")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Napoli")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Genk"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Napoli"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Genk")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Salisburgo"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Liverpool")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "F",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Borussia") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Inter") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Slavia") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Inter"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Slavia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Borussia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Slavia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Borussia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Inter")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Slavia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Inter"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Borussia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Slavia")
                                 },
                                  new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Borussia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Inter")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Slavia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Inter")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Borussia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Inter"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Barcelona")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Borussia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Slavia")
                                 }

                             }
                        },
                           new GroupEntity
                        {
                             Name = "G",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Lyon") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Benfica") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Zenit") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lyon"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Zenit")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Benfica"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lyon")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Zenit"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Benfica")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Zenit")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Benfica"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lyon")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Zenit"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia")
                                 },
                                  new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lyon"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Benfica")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Zenit"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lyon")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Benfica")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Benfica"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Zenit")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lyon"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lipsia")
                                 }

                             }
                        },
                           new GroupEntity
                        {
                             Name ="H",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Valencia") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Ajax") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Lylle") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Valencia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Ajax"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lylle")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Valencia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Ajax")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lylle"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Ajax"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lylle"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Valencia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Valencia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lylle")
                                 },
                                  new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Ajax")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Valencia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Lylle"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Ajax")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(21).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Ajax"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Valencia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Chelsea"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Lylle")
                                 }

                             }
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }

    }
}