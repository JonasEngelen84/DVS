using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllClothesQuery(ClothesDbContextFactory clothesDbContextFactory) : IGetAllClothesQuery
    {
        private readonly ClothesDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task<IEnumerable<ClothesModel>> Execute()
        {
            using ClothesDbContext context = _clothesDbContextFactory.Create();

            IEnumerable<ClothesDTO> clothesDTOs = await context.ClothesDb.ToListAsync();

            return clothesDTOs.Select(y => new ClothesModel(y.GuidID, y.ID, y.Name, y.Category, y.Season, y.Comment) { Sizes = y.Sizes } );
        }
    }
}
