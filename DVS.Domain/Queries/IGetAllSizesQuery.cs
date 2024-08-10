using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllSizesQuery
    {
        Task<IEnumerable<SizeModel>> Execute();
    }
}
