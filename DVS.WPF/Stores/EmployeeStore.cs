using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class EmployeeStore(ICreateEmployeeCommand createEmployeeCommand,
                               IUpdateEmployeeCommand updateEmployeeCommand,
                               IDeleteEmployeeCommand deleteEmployeeCommand)
    {
        private readonly ICreateEmployeeCommand _createEmployeeCommand = createEmployeeCommand;
        private readonly IUpdateEmployeeCommand _updateEmployeeCommand = updateEmployeeCommand;
        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand = deleteEmployeeCommand;

        private readonly List<Employee> _employees = [];
        public IEnumerable<Employee> Employees => _employees;

        public event Action<Employee> EmployeeAdded;
        public event Action<Employee> EmployeeUpdated;
        public event Action<string> EmployeeDeleted;

        public void Load(List<Employee> employees)
        {
            _employees.Clear();

            if (employees != null)
            {
                _employees.AddRange(employees);
            }
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

            int index = _employees.FindIndex(e => e.Id == updatedEmployee.Id);

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

            int index = _employees.FindIndex(e => e.Id == employee.Id);

            if (index != -1)
            {
                _employees.RemoveAll(e => e.Id == employee.Id);
            }
            else
            {
                throw new InvalidOperationException("Löschen des Mitarbeiters nicht möglich.");
            }
            
            EmployeeDeleted?.Invoke(employee.Id);
        }
    }
}
