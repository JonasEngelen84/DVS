using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesSizeCommands
{
    public class CreateClothesSizeCommand(DVSDbContextFactory contextFactory) : ICreateClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(ClothesSize clothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            context.Sizes.Attach(clothesSize.Size);
            context.Clothes.Attach(clothesSize.Clothes);
            context.Categories.Attach(clothesSize.Clothes.Category);
            context.Seasons.Attach(clothesSize.Clothes.Season);
            context.ClothesSizes.Add(clothesSize);

            await context.SaveChangesAsync();
        }
    }
}
