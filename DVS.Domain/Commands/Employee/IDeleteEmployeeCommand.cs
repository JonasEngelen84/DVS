namespace DVS.Domain.Commands.Employee
{
    public interface IDeleteEmployeeCommand
    {
        Task Execute(Guid guidID);
    }
}
