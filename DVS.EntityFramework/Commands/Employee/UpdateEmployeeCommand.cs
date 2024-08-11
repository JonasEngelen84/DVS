using DVS.Domain.Commands.Employee;
using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Employee
{
    public class UpdateEmployeeCommand(DVSDbContextFactory dVSDbContextFactory) : IUpdateEmployeeCommand
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task Execute(Domain.Models.Employee employee)
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            EmployeeDTO employeeDTO = new()
            {
                GuidID = employee.GuidID,
                ID = employee.ID,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Comment = employee.Comment,
                Clothes = employee.Clothes
            };

            context.Employees.Update(employeeDTO);
            await context.SaveChangesAsync();
        }
    }
}
