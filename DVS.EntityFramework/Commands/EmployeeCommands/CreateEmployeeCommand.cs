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
                //context.Clothes.Attach(ecs.ClothesSize.Clothes);
                //context.ClothesSizes.Attach(ecs.ClothesSize);

                //var localClothes = context.Clothes.Local.FirstOrDefault(c => c.GuidId == ecs.ClothesSize.Clothes.GuidId);
                //if (localClothes != null)
                //{
                //    context.Entry(localClothes).State = EntityState.Detached;
                //}

                //var localClothesSize = context.ClothesSizes.Local.FirstOrDefault(cs => cs.GuidId == ecs.ClothesSize.GuidId);
                //if (localClothesSize != null)
                //{
                //    context.Entry(localClothesSize).State = EntityState.Detached;
                //}
            }

            context.Employees.Add(employee);

            await context.SaveChangesAsync();
        }
    }
}
