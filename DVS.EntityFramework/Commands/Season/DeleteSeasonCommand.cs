using DVS.Domain.Commands.Season;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Season
{
    public class DeleteSeasonCommand(SeasonDbContextFactory seasonDbContextFactory) : IDeleteSeasonCommand
    {
        private readonly SeasonDbContextFactory _seasonDbContextFactory = seasonDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using SeasonDbContext context = _seasonDbContextFactory.Create();

            SeasonDTO seasonDTO = new()
            {
                GuidID = guidID
            };

            context.SeasonDb.Update(seasonDTO);
            await context.SaveChangesAsync();
        }
    }
}
