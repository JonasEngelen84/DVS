using DVS.Domain.Commands.Clothes;
using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class CreateClothesCommand(DVSDbContextFactory clothesDbContextFactory) : ICreateClothesCommand
    {
        private readonly DVSDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task Execute(ClothesModel clothes)
        {
            using DVSDbContext context = _clothesDbContextFactory.Create();

            ClothesDTO clothesDTO = new()
            {
                GuidID = clothes.GuidID,
                ID = clothes.ID,
                Name = clothes.Name,
                Category = clothes.Category,
                Season = clothes.Season,
                Comment = clothes.Comment,
                Sizes = clothes.Sizes,
            };

            context.Clothes.Add(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
