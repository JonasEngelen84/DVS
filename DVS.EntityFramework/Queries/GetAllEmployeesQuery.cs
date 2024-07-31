using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllEmployeesQuery(EmployeeDbContextFactory employeeDbContextFactory) : IGetAllEmployeesQuery
    {
        private readonly EmployeeDbContextFactory _employeeDbContextFactory = employeeDbContextFactory;

        public async Task<IEnumerable<EmployeeModel>> Execute()
        {
            using EmployeeDbContext context = _employeeDbContextFactory.Create();

            IEnumerable<EmployeeDTO> employeeDTOs = await context.EmployeeDb.ToListAsync();

            return employeeDTOs.Select(y => new EmployeeModel(y.GuidID, y.ID, y.Lastname, y.Firstname, y.Comment) { Clothes = y.Clothes } );
        }
    }
}
