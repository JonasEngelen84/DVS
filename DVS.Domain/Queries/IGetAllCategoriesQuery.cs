using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllCategoriesQuery
    {
        Task<IEnumerable<CategoryModel>> Execute();
    }
}
