using DVS.Domain.Models;
using DVS.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllClothesQuery(DVSDbContextFactory clothesDbContextFactory) : IGetAllClothesQuery
    {
        private readonly DVSDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task<IEnumerable<Clothes>> Execute()
        {
            using DVSDbContext context = _clothesDbContextFactory.Create();

            var actualClothes = await context.Clothes
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Sizes)
                    .ThenInclude(cs => cs.Size)
                .Include(c => c.Sizes)
                    .ThenInclude(cs => cs.Quantity)
                .ToListAsync();

            return actualClothes;
        }
    }
}
