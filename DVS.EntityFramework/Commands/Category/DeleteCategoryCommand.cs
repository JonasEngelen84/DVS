using DVS.Domain.Commands.Category;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Category
{
    public class DeleteCategoryCommand(CategoryDbContextFactory categoryDbContextFactory) : IDeleteCategoryCommand
    {
        private readonly CategoryDbContextFactory _categoryDbContextFactory = categoryDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using CategoryDbContext context = _categoryDbContextFactory.Create();

            CategoryDTO categoryDTO = new()
            {
                GuidID = guidID
            };

            context.CategoryDb.Remove(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
