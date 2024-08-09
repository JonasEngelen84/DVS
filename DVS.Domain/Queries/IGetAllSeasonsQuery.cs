using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllSeasonsQuery
    {
        Task<IEnumerable<Season>> Execute();
    }
}
