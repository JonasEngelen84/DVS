namespace DVS.Domain.Commands.EmployeeClothesSize
{
    public interface IDeleteEmployeeClothesSizeCommand
    {
        Task Execute(Guid guidID);
    }
}
