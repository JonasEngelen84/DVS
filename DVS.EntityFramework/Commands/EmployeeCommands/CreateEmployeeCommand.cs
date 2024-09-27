using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.EmployeeCommands
{
    public class CreateEmployeeCommand(DVSDbContextFactory contextFactory) : ICreateEmployeeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Employee employee)
        {
            using DVSDbContext context = _contextFactory.Create();

            //Employee employee = new()
            //{
            //    GuidID = employee.GuidID,
            //    ID = employee.ID,
            //    Lastname = employee.Lastname,
            //    Firstname = employee.Firstname,
            //    Comment = employee.Comment,
            //    Clothes = employee.Clothes
            //};

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }
    }
}
