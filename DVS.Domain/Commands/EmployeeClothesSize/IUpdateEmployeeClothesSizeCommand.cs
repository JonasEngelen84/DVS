namespace DVS.Domain.Commands.EmployeeClothesSize
{
    public interface IUpdateEmployeeClothesSizeCommand
    {
        Task Execute(Models.EmployeeClothesSize employeeClothesSize);
    }
}
