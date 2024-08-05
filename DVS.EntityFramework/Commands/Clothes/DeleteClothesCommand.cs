using DVS.Domain.Commands.Clothes;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Clothes
{
    public class DeleteClothesCommand(DVSDbContextFactory clothesDbContextFactory) : IDeleteClothesCommand
    {
        private readonly DVSDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _clothesDbContextFactory.Create();

            ClothesDTO clothesDTO = new()
            {
                GuidID = guidID
            };

            context.Clothes.Remove(clothesDTO);
            await context.SaveChangesAsync();
        }
    }
}
