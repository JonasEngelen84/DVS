using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.EmployeeCommands
{
    public class DeleteEmployeeCommand(DVSDbContextFactory contextFactory) : IDeleteEmployeeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(string Id)
        {
            using DVSDbContext context = _contextFactory.Create();
            Employee? employee = await context.Employees.FindAsync(Id);
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
        }
    }
}
