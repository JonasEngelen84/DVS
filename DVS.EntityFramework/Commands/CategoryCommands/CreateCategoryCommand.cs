using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.CategoryCommands
{
    public class CreateCategoryCommand(DVSDbContextFactory contextFactory) : ICreateCategoryCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Category category)
        {
            using DVSDbContext context = _contextFactory.Create();

            //Category newCategory = new(category.GuidID, category.Name)
            //{
            //    Clothes = category.Clothes
            //};

            context.Categories.Add(category);

            await context.SaveChangesAsync();
        }
    }
}
