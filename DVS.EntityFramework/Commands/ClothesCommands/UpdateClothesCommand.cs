using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesCommands
{
    public class UpdateClothesCommand(DVSDbContextFactory contextFactory) : IUpdateClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Clothes editedClothes)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingClothes = await context.Clothes.FindAsync(editedClothes.Id);

            if (existingClothes != null)
            {
                context.Entry(existingClothes).CurrentValues.SetValues(editedClothes);
            }

            await context.SaveChangesAsync();
        }
    }
}
