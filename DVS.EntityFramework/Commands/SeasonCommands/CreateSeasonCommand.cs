using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.SeasonCommands
{
    public class CreateSeasonCommand(DVSDbContextFactory contextFactory) : ICreateSeasonCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Season season)
        {
            using DVSDbContext context = _contextFactory.Create();

            //Season season = new()
            //{
            //    GuidID = season.GuidID,
            //    Name = season.Name,
            //    Clothes = season.Clothes
            //};

            context.Seasons.Add(season);
            await context.SaveChangesAsync();
        }
    }
}
