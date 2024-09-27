using DVS.Domain.Models;

namespace DVS.Domain.Commands.EmployeeCommands
{
    public interface ICreateEmployeeCommand
    {
        Task Execute(Employee employee);
    }
}
