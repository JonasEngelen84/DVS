using DVS.Domain.Commands.Season;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Season
{
    public class DeleteSeasonCommand(DVSDbContextFactory contextFactory) : IDeleteSeasonCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _contextFactory.Create();

            SeasonDTO seasonDTO = new()
            {
                GuidID = guidID
            };

            context.Seasons.Update(seasonDTO);
            await context.SaveChangesAsync();
        }
    }
}
