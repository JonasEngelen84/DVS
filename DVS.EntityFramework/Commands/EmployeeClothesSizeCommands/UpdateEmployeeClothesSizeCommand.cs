using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.EmployeeClothesSizeCommands
{
    public class UpdateEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : IUpdateEmployeeClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(EmployeeClothesSize editedEcs)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingEcs = await context.EmployeeClothesSizes.FindAsync(editedEcs.Id);

            if (existingEcs != null)
            {
                context.Entry(existingEcs).CurrentValues.SetValues(editedEcs);
            }
        }
    }
}
