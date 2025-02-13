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

            //context.Entry(updatedClothesSize.Clothes).State = EntityState.Modified;
            //context.Sizes.Attach(updatedClothesSize.Size);
            context.ClothesSizes.Add(clothesSize);

            await context.SaveChangesAsync();
        }
    }
}
