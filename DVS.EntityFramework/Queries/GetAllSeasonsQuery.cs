using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllSeasonsQuery(SeasonDbContextFactory seasonDbContextFactory) : IGetAllSeasonsQuery
    {
        private readonly SeasonDbContextFactory _seasonDbContextFactory = seasonDbContextFactory;

        public async Task<IEnumerable<SeasonModel>> Execute()
        {
            using SeasonDbContext context = _seasonDbContextFactory.Create();

            IEnumerable<SeasonDTO> seasonDTOs = await context.SeasonDb.ToListAsync();

            return seasonDTOs.Select(y => new SeasonModel(y.GuidID, y.Name));
        }
    }
}
