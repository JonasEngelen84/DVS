using DVS.Domain.Commands.Category;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Category
{
    public class UpdateCategoryCommand(DVSDbContextFactory contextFactory) : IUpdateCategoryCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.Category category)
        {
            using DVSDbContext context = _contextFactory.Create();

            CategoryDTO categoryDTO = new()
            {
                GuidID = category.GuidID,
                Name = category.Name,
                Clothes = category.Clothes
            };

            context.Categories.Update(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
