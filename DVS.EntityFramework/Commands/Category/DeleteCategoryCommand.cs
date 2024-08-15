using DVS.Domain.Commands.Category;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Category
{
    public class DeleteCategoryCommand(DVSDbContextFactory contextFactory) : IDeleteCategoryCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _contextFactory.Create();

            CategoryDTO categoryDTO = new()
            {
                GuidID = guidID
            };

            context.Categories.Remove(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
