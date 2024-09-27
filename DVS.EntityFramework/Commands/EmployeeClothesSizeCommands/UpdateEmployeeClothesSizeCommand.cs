using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.EmployeeClothesSizeCommands
{
    public class UpdateEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : IUpdateEmployeeClothesSizeCommand
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

            context.EmployeeClothesSizes.Update(employeeClothesSize);
            await context.SaveChangesAsync();
        }
    }
}
