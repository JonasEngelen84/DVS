using DVS.Domain.Commands.Season;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Season
{
    public class UpdateSeasonCommand(DVSDbContextFactory contextFactory) : IUpdateSeasonCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.Season season)
        {
            using DVSDbContext context = _contextFactory.Create();

            SeasonDTO seasonDTO = new()
            {
                GuidID = season.GuidID,
                Name = season.Name,
                Clothes = season.Clothes
            };

            context.Seasons.Update(seasonDTO);
            await context.SaveChangesAsync();
        }
    }
}
