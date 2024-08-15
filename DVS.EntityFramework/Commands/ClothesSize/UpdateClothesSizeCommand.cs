using DVS.Domain.Commands.ClothesSize;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.ClothesSize
{
    public class UpdateClothesSizeCommand(DVSDbContextFactory contextFactory) : IUpdateClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.ClothesSize clothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            ClothesSizeDTO clothesSizeDTO = new()
            {
                GuidID = clothesSize.GuidID,
                ClothesGuidID = clothesSize.ClothesGuidID,
                SizeGuidID = clothesSize.SizeGuidID,
                Quantity = clothesSize.Quantity,
                Comment = clothesSize.Comment,
                EmployeeClothesSizes = clothesSize.EmployeeClothesSizes
            };

            context.ClothesSizes.Update(clothesSizeDTO);
            await context.SaveChangesAsync();
        }
    }
}
