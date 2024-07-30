using DVS.Domain.Models;

namespace DVS.Domain.Commands.Employee
{
    public interface IUpdateEmployeeCommand
    {
        Task Execute(EmployeeModel employee);
    }
}
