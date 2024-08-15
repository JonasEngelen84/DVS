namespace DVS.Domain.Commands.Employee
{
    public interface IUpdateEmployeeCommand
    {
        Task Execute(Models.Employee employee);
    }
}
