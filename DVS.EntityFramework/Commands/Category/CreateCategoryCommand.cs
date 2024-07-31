using DVS.Domain.Commands.Category;
using DVS.Domain.Models;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Category
{
    public class CreateCategoryCommand(CategoryDbContextFactory categoryDbContextFactory) : ICreateCategoryCommand
    {
        private readonly CategoryDbContextFactory _categoryDbContextFactory = categoryDbContextFactory;

        public async Task Execute(CategoryModel category)
        {
            using CategoryDbContext context = _categoryDbContextFactory.Create();

            CategoryDTO categoryDTO = new()
            {
                GuidID = category.GuidID,
                Name = category.Name,
            };

            context.CategoryDb.Add(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
