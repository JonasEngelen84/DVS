using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Commands.ClothesCommands
{
    public class UpdateClothesCommand(DVSDbContextFactory contextFactory) : IUpdateClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Clothes updatedClothes)
        {
            using DVSDbContext context = _contextFactory.Create();

            context.Entry(updatedClothes).State = EntityState.Modified;
            context.Clothes.Update(updatedClothes);

            await context.SaveChangesAsync();
        }
    }
}
