using DVS.Domain.Commands.Category;

namespace DVS.EntityFramework.Commands.Category
{
    public class ClearCategoriesCommand(DVSDbContextFactory dVSDbContextFactory) : IClearCategoriesCommand
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task Execute()
        {
            //using var context = new CategoryDbContext();
            //context.CategoryDb.RemoveRange(context.CategoryDb);
            //await context.SaveChangesAsync();
        }
    }
}
