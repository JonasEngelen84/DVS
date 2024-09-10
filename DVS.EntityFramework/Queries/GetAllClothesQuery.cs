using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DVS.EntityFramework.Queries
{
    public class GetAllClothesQuery(DVSDbContextFactory clothesDbContextFactory) : IGetAllClothesQuery
    {
        private readonly DVSDbContextFactory _clothesDbContextFactory = clothesDbContextFactory;

        public async Task<IEnumerable<Clothes>> Execute()
        {
            using DVSDbContext context = _clothesDbContextFactory.Create();

            IEnumerable<ClothesDTO> clothesDTOs = await context.Clothes.ToListAsync();

            List<Clothes> clothesList = [];

            foreach (var clothesDTO in clothesDTOs)
            {
                CategoryDTO categoryDTO = await context.Categories.FindAsync(clothesDTO.CategoryGuidID);
                Category category = new(categoryDTO.GuidID, categoryDTO.Name);

                SeasonDTO seasonDTO = await context.Seasons.FindAsync(clothesDTO.SeasonGuidID);
                Season season = new(seasonDTO.GuidID, seasonDTO.Name);

                Clothes clothes = new(clothesDTO.GuidID,
                                      clothesDTO.ID,
                                      clothesDTO.Name,
                                      category,
                                      season,
                                      clothesDTO.Comment)
                {
                    Sizes = new ObservableCollection<ClothesSize>(clothesDTO.Sizes)
                };

                clothesList.Add(clothes);
            }

            return clothesList;
        }
    }
}
