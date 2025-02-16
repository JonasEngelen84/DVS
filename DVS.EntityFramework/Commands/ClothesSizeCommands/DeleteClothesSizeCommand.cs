using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesSizeCommands
{
    public class DeleteClothesSizeCommand(DVSDbContextFactory contextFactory) : IDeleteClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(ClothesSize updatedClothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            context.ClothesSizes.Remove(updatedClothesSize);

            await context.SaveChangesAsync();
        }
    }
}
