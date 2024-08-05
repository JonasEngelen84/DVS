using System.Collections.Generic;
using System.Threading.Tasks;
using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllCategoriesQuery
    {
        Task<IEnumerable<CategoryModel>> Execute();
    }
}
