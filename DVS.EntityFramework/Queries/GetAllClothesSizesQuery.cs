using DVS.Domain.Models;
using DVS.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllClothesSizesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllClothesSizesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<ClothesSize>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            var actualClothesSize = await context.ClothesSizes
                .Include(cs => cs.Size)
                .Include(cs => cs.Clothes)
                    .ThenInclude(cs => cs.Category)
                .Include(cs => cs.Clothes)
                    .ThenInclude(cs => cs.Season)
                .Include(cs => cs.Clothes)
                    .ThenInclude(cs => cs.Sizes)
                        .ThenInclude(s => s.Size)
                .Include(cs => cs.EmployeeClothesSizes)
                .ToListAsync();

            return actualClothesSize;
        }
    }
}
