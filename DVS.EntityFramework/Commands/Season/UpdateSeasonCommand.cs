using DVS.Domain.Commands.Season;
using DVS.Domain.Models;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Season
{
    public class UpdateSeasonCommand(SeasonDbContextFactory seasonDbContextFactory) : IUpdateSeasonCommand
    {
        private readonly SeasonDbContextFactory _seasonDbContextFactory = seasonDbContextFactory;

        public async Task Execute(SeasonModel season)
        {
            using SeasonDbContext context = _seasonDbContextFactory.Create();

            SeasonDTO seasonDTO = new()
            {
                GuidID = season.GuidID,
                Name = season.Name,
            };

            context.SeasonDb.Update(seasonDTO);
            await context.SaveChangesAsync();
        }
    }
}
