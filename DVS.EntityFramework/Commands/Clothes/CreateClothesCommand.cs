using DVS.Domain.Commands.Clothes;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class CreateClothesCommand(DVSDbContextFactory contextFactory) : ICreateClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.Clothes clothes)
        {
            using DVSDbContext context = _contextFactory.Create();

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

            context.Clothes.Add(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
