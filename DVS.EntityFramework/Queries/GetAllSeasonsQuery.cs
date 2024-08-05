using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllSeasonsQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllSeasonsQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<SeasonModel>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            IEnumerable<SeasonDTO> seasonDTOs = await context.Seasons.ToListAsync();

            return seasonDTOs.Select(y => new SeasonModel(y.GuidID, y.Name));
        }
    }
}
