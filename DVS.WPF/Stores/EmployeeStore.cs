using DVS.Domain.Commands.EmployeeCommands;
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
            IEnumerable<Employee> employee = await _getAllEmployeesQuery.Execute();

            _employees.Clear();

            if (employee != null)
            {
                _employees.AddRange(employee);
            }

            EmployeesLoaded?.Invoke();
        }

        public async Task Add(Employee employee)
        {
            await _createEmployeeCommand.Execute(employee);

            _employees.Add(employee);

            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(Employee updatedEmployee)
        {
            await _updateEmployeeCommand.Execute(updatedEmployee);

            int index = _employees.FindIndex(y => y.GuidID == updatedEmployee.GuidID);

            if (index != -1)
            {
                _employees[index] = updatedEmployee;
            }
            else
            {
                _employees.Add(updatedEmployee);
            }

            EmployeeUpdated.Invoke(updatedEmployee);
        }

        public async Task Delete(Employee employee)
        {
            await _deleteEmployeeCommand.Execute(employee);

            _employees.RemoveAll(y => y.GuidID == employee.GuidID);

            EmployeeDeleted?.Invoke(employee.GuidID);
        }
    }
}
