using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.ClothesCommands
{
    public class CreateClothesCommand(DVSDbContextFactory contextFactory) : ICreateClothesCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Clothes clothes)
        {
            using DVSDbContext context = _contextFactory.Create();

            foreach (ClothesSize cs in clothes.Sizes)
            {
                context.Sizes.Attach(cs.Size);
            }

            context.Categories.Attach(clothes.Category);
            context.Seasons.Attach(clothes.Season);
            context.Clothes.Add(clothes);

            await context.SaveChangesAsync();
        }
    }
}
