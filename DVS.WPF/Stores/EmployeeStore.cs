using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using System.Windows;

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
            IEnumerable<Employee> employee = [];

            try
            {
                employee = await _getAllEmployeesQuery.Execute();
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der EmployeeClothesSizes von Datenbank ist fehlgeschlagen!", "EmployeeClothesSizesStore, Load", button, icon);
            }

            _employees.Clear();

            if (employee != null)
            {
                _employees.AddRange(employee);
            }

            EmployeesLoaded?.Invoke();
        }

        public async Task Add(Employee employee)
        {
            try
            {
                await _createEmployeeCommand.Execute(employee);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Hinzufügen des Employee in Datenbank ist fehlgeschlagen!", "EmployeeStore, Add", button, icon);
            }

            _employees.Add(employee);

            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(Employee employee)
        {
            try
            {
                //await _updateEmployeeCommand.Execute(employee);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten des Employee in Datenbank ist fehlgeschlagen!", "EmployeeStore, Update", button, icon);
            }

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

        public async Task Delete(Employee employee)
        {
            try
            {
                await _deleteEmployeeCommand.Execute(employee);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Löschen des Employee aus Datenbank ist fehlgeschlagen!", "EmployeeStore, Delete", button, icon);
            }

            _employees.RemoveAll(y => y.GuidID == employee.GuidID);

            EmployeeDeleted?.Invoke(employee.GuidID);
        }
    }
}
