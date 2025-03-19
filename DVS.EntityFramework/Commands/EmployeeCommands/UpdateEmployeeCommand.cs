using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Commands.EmployeeCommands
{
    public class UpdateEmployeeCommand(DVSDbContextFactory contextFactory) : IUpdateEmployeeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Employee editedEmployee)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingEmployee = await context.Employees.FindAsync(editedEmployee.Id);

            if (existingEmployee != null)
            {
                context.Entry(existingEmployee).CurrentValues.SetValues(editedEmployee);
            }
        }
    }
}
