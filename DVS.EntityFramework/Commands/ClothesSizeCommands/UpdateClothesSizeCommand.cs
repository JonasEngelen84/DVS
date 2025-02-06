using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Commands.ClothesSizeCommands
{
    public class UpdateClothesSizeCommand(DVSDbContextFactory contextFactory) : IUpdateClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(ClothesSize clothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingClothesSize = await context.ClothesSizes
                .FirstOrDefaultAsync(cs => cs.GuidId == clothesSize.GuidId);

            if (existingClothesSize != null)
            {
                context.Entry(existingClothesSize).CurrentValues.SetValues(clothesSize);
            }
            else
            {
                context.ClothesSizes.Add(clothesSize);
            }

            await context.SaveChangesAsync();
        }
    }
}
