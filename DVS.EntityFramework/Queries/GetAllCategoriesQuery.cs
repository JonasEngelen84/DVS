using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllCategoriesQuery(CategoryDbContextFactory categoryDbContextFactory) : IGetAllCategoriesQuery
    {
        private readonly CategoryDbContextFactory _categoryDbContextFactory = categoryDbContextFactory;

        public async Task<IEnumerable<CategoryModel>> Execute()
        {
            using CategoryDbContext context = _categoryDbContextFactory.Create();

            IEnumerable<CategoryDTO> categoryDTOs = await context.CategoryDb.ToListAsync();

            return categoryDTOs.Select(y => new CategoryModel(y.GuidID, y.Name));
        }
    }
}
