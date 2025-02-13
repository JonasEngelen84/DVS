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
            return await context.Categories.ToListAsync();
        }
    }
}
