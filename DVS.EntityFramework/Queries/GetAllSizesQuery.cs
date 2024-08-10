using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.Queries
{
    public class GetAllSizesQuery(DVSDbContextFactory dVSDbContextFactory) : IGetAllSizesQuery
    {
        private readonly DVSDbContextFactory _dVSDbContextFactory = dVSDbContextFactory;

        public async Task<IEnumerable<SizeModel>> Execute()
        {
            using DVSDbContext context = _dVSDbContextFactory.Create();

            IEnumerable<SizeModelDTO> sizeDTOs = await context.Sizes.ToListAsync();

            return sizeDTOs.Select(y => new SizeModel(y.GuidID, y.Size, y.IsSizeSystemEU));
        }
    }
}
