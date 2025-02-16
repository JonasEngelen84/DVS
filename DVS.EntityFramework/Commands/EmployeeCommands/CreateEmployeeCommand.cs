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

            Employee newEmployee = new(
                employee.Id,
                employee.Lastname,
                employee.Firstname,
                employee.Comment)
            {
                Clothes = []
            };

            foreach (EmployeeClothesSize ecs in employee.Clothes)
            {
                ClothesSize? existingClothesSize = await context.ClothesSizes.FindAsync(ecs.ClothesSizeGuidId);

                EmployeeClothesSize newEmployeeClothesSize = new(
                    ecs.GuidId,
                    newEmployee,
                    existingClothesSize,
                    ecs.Quantity,
                    ecs.Comment
                );

                newEmployee.Clothes.Add(newEmployeeClothesSize);
            }

            context.Employees.Add(newEmployee);
            await context.SaveChangesAsync();
        }
    }
}
