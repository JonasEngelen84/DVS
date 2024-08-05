using DVS.Domain.Commands.Season;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Season
{
    public class DeleteSeasonCommand(DVSDbContextFactory dVSDbContextFactory) : IDeleteSeasonCommand
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            SeasonDTO seasonDTO = new()
            {
                GuidID = guidID
            };

            context.Seasons.Update(seasonDTO);
            await context.SaveChangesAsync();
        }
    }
}
