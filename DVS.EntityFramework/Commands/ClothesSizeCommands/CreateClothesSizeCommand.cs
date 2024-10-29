using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Commands.ClothesSizeCommands
{
    public class CreateClothesSizeCommand(DVSDbContextFactory contextFactory) : ICreateClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(ClothesSize updatedClothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            context.Entry(updatedClothesSize.Clothes).State = EntityState.Modified;
            context.Sizes.Attach(updatedClothesSize.Size);
            context.ClothesSizes.Add(updatedClothesSize);

            await context.SaveChangesAsync();
        }
    }
}
