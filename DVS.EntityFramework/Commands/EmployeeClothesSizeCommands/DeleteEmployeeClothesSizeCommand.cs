using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.EmployeeClothesSizeCommands
{
    public class DeleteEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : IDeleteEmployeeClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(EmployeeClothesSize employeeClothesSize)
        {            
            using DVSDbContext context = _contextFactory.Create();

            var existingEcs = await context.EmployeeClothesSizes.FindAsync(employeeClothesSize.Id);

            if (existingEcs != null)
            {
                context.EmployeeClothesSizes.Remove(existingEcs);
            }
            
            await context.SaveChangesAsync();
        }
    }
}
