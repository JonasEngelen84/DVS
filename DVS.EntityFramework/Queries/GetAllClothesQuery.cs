using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllClothesQuery(DVSDbContextFactory clothesDbContextFactory) : IGetAllClothesQuery
    {
        private readonly DVSDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task<IEnumerable<ClothesModel>> Execute()
        {
            using DVSDbContext context = _clothesDbContextFactory.Create();

            IEnumerable<ClothesDTO> clothesDTOs = await context.Clothes.ToListAsync();

            return clothesDTOs.Select(y => new ClothesModel(y.GuidID, y.ID, y.Name, y.Category, y.Season, y.Comment) { Sizes = y.Sizes } );
        }
    }
}
