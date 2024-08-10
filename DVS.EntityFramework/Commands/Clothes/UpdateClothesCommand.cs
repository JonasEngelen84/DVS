using DVS.Domain.Commands.Clothes;
using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class UpdateClothesCommand(DVSDbContextFactory clothesDbContextFactory) : IUpdateClothesCommand
    {
        private readonly DVSDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task Execute(Domain.Models.Clothes clothes)
        {
            using DVSDbContext context = _clothesDbContextFactory.Create();

            ClothesDTO clothesDTO = new()
            {
                GuidID = clothes.GuidID,
                ID = clothes.ID,
                Name = clothes.Name,
                CategoryGuidID = clothes.CategoryGuidID,
                SeasonGuidID = clothes.SeasonGuidID,
                Comment = clothes.Comment,
                Sizes = clothes.Sizes,
            };

            context.Clothes.Update(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
