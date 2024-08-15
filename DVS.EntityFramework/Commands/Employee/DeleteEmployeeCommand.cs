using DVS.Domain.Commands.Employee;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Employee
{
    public class DeleteEmployeeCommand(DVSDbContextFactory contextFactory) : IDeleteEmployeeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _contextFactory.Create();

            EmployeeDTO employeeDTO = new()
            {
                GuidID = guidID
            };

            context.Employees.Remove(employeeDTO);
            await context.SaveChangesAsync();
        }
    }
}
