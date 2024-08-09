using DVS.Domain.Commands.Category;
using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Category
{
    public class CreateCategoryCommand(DVSDbContextFactory dVSDbContextFactory) : ICreateCategoryCommand
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task Execute(Domain.Models.Category category)
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            CategoryDTO categoryDTO = new()
            {
                GuidID = category.GuidID,
                Name = category.Name,
            };

            context.Categories.Add(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
