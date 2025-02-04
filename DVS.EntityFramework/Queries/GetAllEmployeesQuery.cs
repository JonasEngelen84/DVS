using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllEmployeesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllEmployeesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<Employee>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            var actualEmployee = await context.Employees
                .Include(e => e.Clothes)
                    .ThenInclude(ecs => ecs.ClothesSize)
                        .ThenInclude(cs => cs.Clothes)
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
                        .ThenInclude(cs => cs.Size)
                .ToListAsync();

            return actualEmployee;
        }
    }
}
