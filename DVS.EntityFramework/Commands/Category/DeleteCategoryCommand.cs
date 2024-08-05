using DVS.Domain.Commands.Category;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Category
{
    public class DeleteCategoryCommand(DVSDbContextFactory dVSDbContextFactory) : IDeleteCategoryCommand
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            CategoryDTO categoryDTO = new()
            {
                GuidID = guidID
            };

            context.Categories.Remove(categoryDTO);
            await context.SaveChangesAsync();
        }
    }
}
