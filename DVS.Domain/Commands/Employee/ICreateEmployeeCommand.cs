using DVS.Domain.Models;

namespace DVS.Domain.Commands.Employee
{
    public interface ICreateEmployeeCommand
    {
        Task Execute(EmployeeModel employee);
    }
}
