using DVS.Domain.Commands.EmployeeClothesSize;
using DVS.EntityFramework.DTOs;

namespace DVS.EntityFramework.Commands.EmployeeClothesSize
{
    public class DeleteEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : IDeleteEmployeeClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Guid guidID)
        {
            using DVSDbContext context = _contextFactory.Create();

            EmployeeClothesSizeDTO employeeClothesSizeDTO = new()
            {
                GuidID = guidID
            };

            context.EmployeeClothesSizes.Remove(employeeClothesSizeDTO);
            await context.SaveChangesAsync();
        }
    }
}
