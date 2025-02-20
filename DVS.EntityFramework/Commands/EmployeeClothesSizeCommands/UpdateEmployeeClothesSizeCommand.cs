using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Commands.EmployeeClothesSizeCommands
{
    public class UpdateEmployeeClothesSizeCommand(DVSDbContextFactory contextFactory) : IUpdateEmployeeClothesSizeCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(EmployeeClothesSize editedEcs)
        {
            using DVSDbContext context = _contextFactory.Create();

            var existingEcs = await context.EmployeeClothesSizes.FindAsync(editedEcs.GuidId);

            if (existingEcs != null)
            {
                context.Entry(existingEcs).CurrentValues.SetValues(editedEcs);
            }

            await context.SaveChangesAsync();
        }
    }
}
