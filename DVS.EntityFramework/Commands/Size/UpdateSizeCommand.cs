using DVS.Domain.Commands.Size;
using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Size
{
    public class UpdateSizeCommand(DVSDbContextFactory contextFactory) : IUpdateSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Domain.Models.SizeModel size)
        {
            using DVSDbContext context = _contextFactory.Create();

            SizeModelDTO sizeModelDTO = new()
            {
                GuidID = size.GuidID,
                Size = size.Size,
                Quantity = size.Quantity,
                IsSizeSystemEU = size.IsSizeSystemEU,
                IsSelected = size.IsSelected,
                ClothesSizes = size.ClothesSizes
            };

            context.Sizes.Update(sizeModelDTO);
            await context.SaveChangesAsync();
        }
    }
}
