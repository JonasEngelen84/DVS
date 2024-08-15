namespace DVS.Domain.Commands.Employee
{
    public interface ICreateEmployeeCommand
    {
        Task Execute(Models.Employee employee);
    }
}
