using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.CategoryCommands
{
    public class UpdateCategoryCommand(DVSDbContextFactory contextFactory) : IUpdateCategoryCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Category editedCategory)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingCategory = await context.Categories.FindAsync(editedCategory.Id);

            if (existingCategory != null)
            {
                context.Entry(existingCategory).CurrentValues.SetValues(editedCategory);
            }
        }
    }
}
