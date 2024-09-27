using DVS.Domain.Models;

namespace DVS.Domain.Commands.EmployeeClothesSizeCommands
{
    public interface IUpdateEmployeeClothesSizeCommand
    {
        Task Execute(EmployeeClothesSize employeeClothesSize);
    }
}
