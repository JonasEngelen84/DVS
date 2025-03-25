using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class EmployeeStore(ICreateEmployeeCommand createEmployeeCommand,
                               IDeleteEmployeeCommand deleteEmployeeCommand)
    {
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
            await createEmployeeCommand.Execute(employee);

            _employees.Add(employee);

            EmployeeAdded?.Invoke(employee);
        }

        public void Update(Employee editedEmployee)
        {
            int index = _employees.FindIndex(e => e.Id == editedEmployee.Id);

            if (index != -1)
            {
                _employees[index] = editedEmployee;
            }
            else
            {
                _employees.Add(editedEmployee);
            }

            EmployeeUpdated.Invoke(editedEmployee);

            editedEmployee.IsDirty = true;
        }

        public async Task Delete(Employee employee)
        {
            await deleteEmployeeCommand.Execute(employee);

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
