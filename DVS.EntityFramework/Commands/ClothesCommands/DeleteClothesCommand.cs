using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesCommands
{
    public class DeleteClothesCommand(DVSDbContextFactory contextFactory) : IDeleteClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Clothes clothes)
        {
            using DVSDbContext context = _contextFactory.Create();

            //Clothes clothes = new()
            //{
            //    GuidID = guidID
            //};

            context.Clothes.Remove(clothes);

            await context.SaveChangesAsync();
        }
    }
}
