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

            //Clothes clothes = new()
            //{
            //    GuidID = clothes.GuidID,
            //    ID = clothes.ID,
            //    Name = clothes.Name,
            //    CategoryGuidID = clothes.CategoryGuidID,
            //    SeasonGuidID = clothes.SeasonGuidID,
            //    Comment = clothes.Comment,
            //    Sizes = clothes.Sizes,
            //};

            context.Clothes.Add(clothes);

            await context.SaveChangesAsync();
        }
    }
}
