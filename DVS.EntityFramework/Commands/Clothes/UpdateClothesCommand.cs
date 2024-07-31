using DVS.Domain.Commands.Clothes;
using DVS.Domain.Models;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class UpdateClothesCommand(ClothesDbContextFactory clothesDbContextFactory) : IUpdateClothesCommand
    {
        private readonly ClothesDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task Execute(ClothesModel clothes)
        {
            using ClothesDbContext context = _clothesDbContextFactory.Create();

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

            context.ClothesDb.Update(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
