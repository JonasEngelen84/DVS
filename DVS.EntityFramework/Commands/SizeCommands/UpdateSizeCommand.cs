using DVS.Domain.Commands.SizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.SizeCommands
{
    public class UpdateSizeCommand(DVSDbContextFactory contextFactory) : IUpdateSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(SizeModel size)
        {
            using DVSDbContext context = _contextFactory.Create();

            //SizeModel sizeModel = new()
            //{
            //    GuidID = size.GuidID,
            //    Size = size.Size,
            //    Quantity = size.Quantity,
            //    IsSizeSystemEU = size.IsSizeSystemEU,
            //    IsSelected = size.IsSelected,
            //    ClothesSizes = size.ClothesSizes
            //};

            context.Sizes.Update(size);
            await context.SaveChangesAsync();
        }
    }
}
