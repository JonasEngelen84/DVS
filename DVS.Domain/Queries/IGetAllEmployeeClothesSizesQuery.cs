using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllEmployeeClothesSizesQuery
    {
        Task<IEnumerable<EmployeeClothesSize>> Execute();
    }
}
