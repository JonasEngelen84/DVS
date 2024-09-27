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

            //EmployeeClothesSize employeeClothesSize = new()
            //{
            //    GuidID = employeeClothesSize.GuidID,
            //    EmployeeGuidID = employeeClothesSize.EmployeeGuidID,
            //    ClothesSizeGuidID = employeeClothesSize.ClothesSizeGuidID,
            //    Quantity = employeeClothesSize.Quantity,
            //    Comment = employeeClothesSize.Comment
            //};

            context.EmployeeClothesSizes.Add(employeeClothesSize);
            await context.SaveChangesAsync();
        }
    }
}
