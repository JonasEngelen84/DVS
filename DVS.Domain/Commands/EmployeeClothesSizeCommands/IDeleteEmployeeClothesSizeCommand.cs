using DVS.Domain.Models;

namespace DVS.Domain.Commands.EmployeeClothesSizeCommands
{
    public interface IDeleteEmployeeClothesSizeCommand
    {
        Task Execute(EmployeeClothesSize employeeClothesSize);
    }
}
