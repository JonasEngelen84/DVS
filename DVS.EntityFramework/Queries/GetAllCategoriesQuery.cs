using DVS.Domain.Models;
using DVS.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllCategoriesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllCategoriesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<Category>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            var actualCategory = await context.Categories
                .Include(c => c.Clothes)
                .ToListAsync();

            return actualCategory;
        }
    }
}
