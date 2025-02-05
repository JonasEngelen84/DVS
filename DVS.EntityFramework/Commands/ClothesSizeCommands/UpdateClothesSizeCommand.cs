using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesSizeCommands
{
    public class UpdateClothesSizeCommand(DVSDbContextFactory contextFactory) : IUpdateClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(ClothesSize clothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();
            context.ClothesSizes.Attach(clothesSize);
            context.ClothesSizes.Update(clothesSize);
            await context.SaveChangesAsync();
        }
    }
}
