using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllClothesQuery
    {
        Task<IEnumerable<ClothesModel>> Execute();
    }
}
