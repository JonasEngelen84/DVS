using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesSizeCommands
{
    public class DeleteClothesSizeCommand(DVSDbContextFactory contextFactory) : IDeleteClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(ClothesSize clothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            //ClothesSize clothesSizeDTO = new()
            //{
            //    GuidID = guidID
            //};

            context.ClothesSizes.Remove(clothesSize);
            await context.SaveChangesAsync();
        }
    }
}
