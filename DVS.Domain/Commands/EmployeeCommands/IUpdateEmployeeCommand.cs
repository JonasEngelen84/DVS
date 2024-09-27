using DVS.Domain.Models;

namespace DVS.Domain.Commands.EmployeeCommands
{
    public interface IUpdateEmployeeCommand
    {
        Task Execute(Employee employee);
    }
}
