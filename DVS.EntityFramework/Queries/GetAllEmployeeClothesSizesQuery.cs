using DVS.Domain.Models;
using DVS.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllEmployeeClothesSizesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllEmployeeClothesSizesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<EmployeeClothesSize>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

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
                        .ThenInclude(c => c.Sizes)
                .Include(ecs => ecs.ClothesSize)
                    .ThenInclude(cs => cs.Size)
                .ToListAsync();

            return actualEmployeeClothesSize;
        }
    }
}
