using DVS.Domain.Commands.Employee;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class EmployeeStore(IGetAllEmployeesQuery getAllEmployeesQuery,
                               ICreateEmployeeCommand createEmployeeCommand,
                               IUpdateEmployeeCommand updateEmployeeCommand,
                               IDeleteEmployeeCommand deleteEmployeeCommand)
    {
        private readonly IGetAllEmployeesQuery _getAllEmployeesQuery = getAllEmployeesQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand = createEmployeeCommand;
        private readonly IUpdateEmployeeCommand _updateEmployeeCommand = updateEmployeeCommand;
        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand = deleteEmployeeCommand;

        private readonly List<Employee> _employees = [];
        public IEnumerable<Employee> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<Employee> EmployeeAdded;
        public event Action<Employee> EmployeeUpdated;
        public event Action<Guid> EmployeeDeleted;

        public async Task Load()
        {
            //await _getAllEmployeesQuery.Execute();

            EmployeesLoaded?.Invoke();
        }

        public async Task Add(Employee employee)
        {
            //await _createEmployeeCommand.Execute(employee);

            _employees.Add(employee);
            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(Employee employee)
        {
            //await _updateEmployeeCommand.Execute(employee);

            int index = _employees.FindIndex(y => y.GuidID == employee.GuidID);

            if (index != -1)
            {
                _employees[index] = employee;
            }
            else
            {
                _employees.Add(employee);
            }

            EmployeeUpdated.Invoke(employee);
        }

        public async Task Delete(Guid guidID)
        {
            //await _deleteEmployeeCommand.Execute(guidID);

            _employees.RemoveAll(y => y.GuidID == guidID);
            EmployeeDeleted?.Invoke(guidID);
        }
    }
}
