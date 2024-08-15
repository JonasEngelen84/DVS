using DVS.Domain.Commands.ClothesSize;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.ClothesSize
{
    public class DeleteClothesSizeCommand(DVSDbContextFactory contextFactory) : IDeleteClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _contextFactory.Create();

            ClothesSizeDTO clothesSizeDTO = new()
            {
                GuidID = guidID
            };

            context.ClothesSizes.Remove(clothesSizeDTO);
            await context.SaveChangesAsync();
        }
    }
}
