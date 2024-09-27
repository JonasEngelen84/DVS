using DVS.Domain.Models;

namespace DVS.Domain.Commands.EmployeeCommands
{
    public interface IDeleteEmployeeCommand
    {
        Task Execute(Employee employee);
    }
}
