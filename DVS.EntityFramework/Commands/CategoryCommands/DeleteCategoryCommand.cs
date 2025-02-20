using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.CategoryCommands
{
    public class DeleteCategoryCommand(DVSDbContextFactory contextFactory) : IDeleteCategoryCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Category category)
        {
            using DVSDbContext context = _contextFactory.Create();

            context.Categories.Remove(category);

            await context.SaveChangesAsync();
        }
    }
}
