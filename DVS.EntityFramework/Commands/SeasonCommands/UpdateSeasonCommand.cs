using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.SeasonCommands
{
    public class UpdateSeasonCommand(DVSDbContextFactory contextFactory) : IUpdateSeasonCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Season editedSeason)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingSeason = await context.Seasons.FindAsync(editedSeason.Id);

            if (existingSeason != null)
            {
                context.Entry(existingSeason).CurrentValues.SetValues(editedSeason);
            }
        }
    }
}
