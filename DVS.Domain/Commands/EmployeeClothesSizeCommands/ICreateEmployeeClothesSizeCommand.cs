using DVS.Domain.Models;

namespace DVS.Domain.Commands.EmployeeClothesSizeCommands
{
    public interface ICreateEmployeeClothesSizeCommand
    {
        Task Execute(EmployeeClothesSize employeeClothesSize);
    }
}
