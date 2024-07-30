using DVS.Domain.Models;

namespace DVS.Domain.Queries
{
    public interface IGetAllEmployeesQuery
    {
        Task<IEnumerable<EmployeeModel>> Execute();
    }
}
