using DVS.Domain.Models;
using DVS.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Services
{
    public class DataLoaderService(DVSDbContextFactory contextFactory) : IDataLoaderService
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task<List<Category>> LoadCategoriesAsync()
        {
            using var context = _contextFactory.Create();
            var categories = await context.Categories.ToListAsync();

            return categories;
        }
        public async Task<List<Season>> LoadSeasonsAsync()
        {
            using var context = _contextFactory.Create();
            var seasons = await context.Seasons.ToListAsync();

            return seasons;
        }
        public async Task<List<Clothes>> LoadClothesAsync()
        {
            using var context = _contextFactory.Create();
            var actualClothes = await context.Clothes
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Sizes)
                .ToListAsync();

            return actualClothes;
        }
        public async Task<List<Employee>> LoadEmployeesAsync()
        {
            using var context = _contextFactory.Create();
            var actualEmployee = await context.Employees
                .Include(e => e.Clothes)
                    .ThenInclude(ecs => ecs.ClothesSize)
                        .ThenInclude(cs => cs.Clothes)
                .Include(e => e.Clothes)
                    .ThenInclude(ecs => ecs.ClothesSize)
                        .ThenInclude(cs => cs.Clothes)
                            .ThenInclude(c => c.Category)
                .Include(e => e.Clothes)
                    .ThenInclude(ecs => ecs.ClothesSize)
                        .ThenInclude(cs => cs.Clothes)
                            .ThenInclude(c => c.Season)
                .Include(e => e.Clothes)
                    .ThenInclude(ecs => ecs.ClothesSize)
                .ToListAsync();

            return actualEmployee;
        }
        public async Task<List<ClothesSize>> LoadClothesSizesAsync()
        {
            using var context = _contextFactory.Create();
            var actualClothesSize = await context.ClothesSizes
                .Include(cs => cs.Clothes)
                    .ThenInclude(cs => cs.Category)
                .Include(cs => cs.Clothes)
                    .ThenInclude(cs => cs.Season)
                .Include(cs => cs.Clothes)
                .Include(cs => cs.EmployeeClothesSizes)
                .ToListAsync();

            return actualClothesSize;
        }
        public async Task<List<EmployeeClothesSize>> LoadEmployeeClothesSizesAsync()
        {
            using var context = _contextFactory.Create();
            var actualEmployeeClothesSize = await context.EmployeeClothesSizes
                .Include(ecs => ecs.Employee)
                    .ThenInclude(e => e.Clothes)
                .Include(ecs => ecs.ClothesSize)
                    .ThenInclude(cs => cs.Clothes)
                        .ThenInclude(c => c.Category)
                .Include(ecs => ecs.ClothesSize)
                    .ThenInclude(cs => cs.Clothes)
                        .ThenInclude(c => c.Season)
                .Include(ecs => ecs.ClothesSize)
                    .ThenInclude(cs => cs.Clothes)
                .Include(ecs => ecs.ClothesSize)
                .ToListAsync();

            return actualEmployeeClothesSize;
        }
    }
}
