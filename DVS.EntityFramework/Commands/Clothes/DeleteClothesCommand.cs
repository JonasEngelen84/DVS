using DVS.Domain.Commands.Clothes;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class DeleteClothesCommand(ClothesDbContextFactory clothesDbContextFactory) : IDeleteClothesCommand
    {
        private readonly ClothesDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using ClothesDbContext context = _clothesDbContextFactory.Create();

            ClothesDTO clothesDTO = new()
            {
                GuidID = guidID
            };

            context.ClothesDb.Remove(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
