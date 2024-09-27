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

            //EmployeeClothesSize employeeClothesSize = new()
            //{
            //    GuidID = guidID
            //};

            context.EmployeeClothesSizes.Remove(employeeClothesSize);
            await context.SaveChangesAsync();
        }
    }
}
