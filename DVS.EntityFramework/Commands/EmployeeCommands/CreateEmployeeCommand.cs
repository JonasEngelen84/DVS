using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
                var localClothes = context.Clothes.Local.FirstOrDefault(c => c.GuidID == ecs.ClothesSize.Clothes.GuidID);
                if (localClothes != null)
                {
                    context.Entry(localClothes).State = EntityState.Detached;
                }

                var localClothesSize = context.ClothesSizes.Local.FirstOrDefault(cs => cs.GuidID == ecs.ClothesSize.GuidID);
                if (localClothesSize != null)
                {
                    context.Entry(localClothesSize).State = EntityState.Detached;
                }

                //context.Clothes.Attach(ecs.ClothesSize.Clothes);
                //context.ClothesSizes.Attach(ecs.ClothesSize);
            }

            context.Employees.Add(employee);

            await context.SaveChangesAsync();
        }
    }
}
