namespace DVS.Domain.Commands.EmployeeClothesSize
{
    public interface ICreateEmployeeClothesSizeCommand
    {
        Task Execute(Models.EmployeeClothesSize employeeClothesSize);
    }
}
