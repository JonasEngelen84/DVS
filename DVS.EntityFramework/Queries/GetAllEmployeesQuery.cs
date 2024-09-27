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
                .Include(s => s.Clothes)
                .ToListAsync();

            return actualEmployee;
        }
    }
}
