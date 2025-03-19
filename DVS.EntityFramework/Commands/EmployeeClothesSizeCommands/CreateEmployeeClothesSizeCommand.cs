using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.EmployeeClothesSizeCommands
{
    public class CreateEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : ICreateEmployeeClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(EmployeeClothesSize employeeClothesSize)
        {
            using DVSDbContext context = _contextFactory.Create();

            Employee? existingEmployee = await context.Employees.FindAsync(employeeClothesSize.EmployeeId);
            ClothesSize? existingClothesSize = await context.ClothesSizes.FindAsync(employeeClothesSize.ClothesSizeGuidId);

            EmployeeClothesSize newEmployeeClothesSize = new(
                employeeClothesSize.Id,
                existingEmployee,
                existingClothesSize,
                employeeClothesSize.Quantity,
                employeeClothesSize.Comment);

            context.EmployeeClothesSizes.Add(newEmployeeClothesSize);
            await context.SaveChangesAsync();
        }
    }
}
