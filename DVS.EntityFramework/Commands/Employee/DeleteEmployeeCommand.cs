using DVS.Domain.Commands.Employee;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Employee
{
    public class DeleteEmployeeCommand(EmployeeDbContextFactory employeeDbContextFactory) : IDeleteEmployeeCommand
    {
        private readonly EmployeeDbContextFactory _employeeDbContextFactory = employeeDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using EmployeeDbContext context = _employeeDbContextFactory.Create();

            EmployeeDTO employeeDTO = new()
            {
                GuidID = guidID
            };

            context.EmployeeDb.Remove(employeeDTO);
            await context.SaveChangesAsync();
        }
    }
}
