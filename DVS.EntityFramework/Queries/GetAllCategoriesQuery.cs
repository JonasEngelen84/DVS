using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllCategoriesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllCategoriesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<CategoryModel>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            IEnumerable<CategoryDTO> categoryDTOs = await context.Categories.ToListAsync();

            return categoryDTOs.Select(y => new CategoryModel(y.GuidID, y.Name));
        }
    }
}
