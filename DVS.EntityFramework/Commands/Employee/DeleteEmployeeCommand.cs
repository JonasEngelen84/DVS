using DVS.Domain.Commands.Employee;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Employee
{
    public class DeleteEmployeeCommand(DVSDbContextFactory dVSDbContextFactory) : IDeleteEmployeeCommand
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            EmployeeDTO employeeDTO = new()
            {
                GuidID = guidID
            };

            context.Employees.Remove(employeeDTO);
            await context.SaveChangesAsync();
        }
    }
}
