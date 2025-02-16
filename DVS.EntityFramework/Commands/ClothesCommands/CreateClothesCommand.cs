using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesCommands
{
    public class CreateClothesCommand(DVSDbContextFactory contextFactory) : ICreateClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Clothes clothes)
        {
            using DVSDbContext context = _contextFactory.Create();

            Category? category = await context.Categories.FindAsync(clothes.CategoryGuidId);
            Season? season = await context.Seasons.FindAsync(clothes.SeasonGuidId);

            Clothes newClothes = new(
                clothes.Id,
                clothes.Name,
                category,
                season,
                clothes.Comment)
            {
                Sizes = []
            };

            foreach (ClothesSize cs in clothes.Sizes)
            {
                SizeModel? existingSize = await context.Sizes.FindAsync(cs.Size.GuidId);

                ClothesSize newClothesSize = new(
                    cs.GuidId,
                    newClothes,
                    existingSize,
                    cs.Quantity,
                    cs.Comment);

                newClothes.Sizes.Add(newClothesSize);
            }
            
            context.Clothes.Add(newClothes);
            await context.SaveChangesAsync();
        }
    }
}
