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

            Clothes? existingClothes = await context.Clothes.FindAsync(clothesSize.ClothesId);
            SizeModel? existingSize = await context.Sizes.FindAsync(clothesSize.SizeGuidId);

            ClothesSize newClothesSize = new(
                clothesSize.GuidId,
                existingClothes,
                existingSize,
                existingSize.Quantity,
                clothesSize.Comment)
            {
                EmployeeClothesSizes = []
            };

            context.ClothesSizes.Add(clothesSize);
            await context.SaveChangesAsync();
        }
    }
}
