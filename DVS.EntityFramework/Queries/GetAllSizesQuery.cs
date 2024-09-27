using DVS.Domain.Models;
using DVS.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllSizesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllSizesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<SizeModel>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            var actualSizes = await context.Sizes
                .Include(s => s.ClothesSizes)
                .ToListAsync();

            return actualSizes;
        }
    }
}
