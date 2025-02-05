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
            //context.Employees.Attach(employeeClothesSize.Employee);
            //context.ClothesSizes.Attach(employeeClothesSize.ClothesSize);
            //context.Sizes.Attach(employeeClothesSize.ClothesSize.Size);
            //context.Clothes.Attach(employeeClothesSize.ClothesSize.Clothes);
            //context.Categories.Attach(employeeClothesSize.ClothesSize.Clothes.Category);
            //context.Seasons.Attach(employeeClothesSize.ClothesSize.Clothes.Season);
            context.EmployeeClothesSizes.Add(employeeClothesSize);
            await context.SaveChangesAsync();
        }
    }
}
