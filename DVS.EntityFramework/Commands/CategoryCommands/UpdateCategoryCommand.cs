using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.CategoryCommands
{
    public class UpdateCategoryCommand(DVSDbContextFactory contextFactory) : IUpdateCategoryCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Category category)
        {
            using DVSDbContext context = _contextFactory.Create();

            //Category updatedCategory = new(category.GuidID, category.Name)
            //{
            //    Clothes = category.Clothes
            //};

            context.Categories.Update(category);

            await context.SaveChangesAsync();
        }
    }
}
