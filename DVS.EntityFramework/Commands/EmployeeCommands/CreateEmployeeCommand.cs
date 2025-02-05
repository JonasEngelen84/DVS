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

            foreach (EmployeeClothesSize ecs in employee.Clothes)
            {
                context.Categories.Attach(ecs.ClothesSize.Clothes.Category);
                //context.ClothesSizes.Attach(ecs.ClothesSize);
                //context.Clothes.Attach(ecs.ClothesSize.Clothes);
            }

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }
    }
}
