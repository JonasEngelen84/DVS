using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllCategoriesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllCategoriesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<Category>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            IEnumerable<CategoryDTO> categoryDTOs = await context.Categories.ToListAsync();

            return categoryDTOs.Select(y => new Category(y.GuidID, y.Name));
        }
    }
}
