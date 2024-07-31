using DVS.Domain.Commands.Employee;
using DVS.Domain.Models;
using DVS.EntityFramework.DbContextFactories;
using DVS.EntityFramework.DbContexts;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.Employee
{
    public class UpdateEmployeeCommand(EmployeeDbContextFactory employeeDbContextFactory) : IUpdateEmployeeCommand
    {
        private readonly EmployeeDbContextFactory _employeeDbContextFactory = employeeDbContextFactory;

        public async Task Execute(EmployeeModel employee)
        {
            using EmployeeDbContext context = _employeeDbContextFactory.Create();

            EmployeeDTO employeeDTO = new()
            {
                GuidID = employee.GuidID,
                ID = employee.ID,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Comment = employee.Comment,
                Clothes = employee.Clothes
            };

            context.EmployeeDb.Update(employeeDTO);
            await context.SaveChangesAsync();
        }
    }
}
