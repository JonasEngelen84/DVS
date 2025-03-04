using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class EmployeeStore(ICreateEmployeeCommand createEmployeeCommand,
                               IUpdateEmployeeCommand updateEmployeeCommand,
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

        public async Task Update(Employee updatedEmployee)
        {
            await updateEmployeeCommand.Execute(updatedEmployee);

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

        public async Task Delete(string Id)
        {
            await deleteEmployeeCommand.Execute(Id);

            int index = _employees.FindIndex(e => e.Id == Id);

            if (index != -1)
            {
                _employees.RemoveAll(e => e.Id == Id);
            }
            else
            {
                throw new InvalidOperationException("Löschen des Mitarbeiters nicht möglich.");
            }
            
            EmployeeDeleted?.Invoke(Id);
        }
    }
}
