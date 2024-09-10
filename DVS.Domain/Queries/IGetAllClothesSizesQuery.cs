using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllClothesSizesQuery
    {
        Task<IEnumerable<ClothesSize>> Execute();
    }
}
