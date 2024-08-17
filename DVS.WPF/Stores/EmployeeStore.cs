using DVS.Domain.Commands.Employee;
using DVS.Domain.Commands.EmployeeClothesSize;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class EmployeeStore(IGetAllEmployeesQuery getAllEmployeesQuery,
                               ICreateEmployeeCommand createEmployeeCommand,
                               IUpdateEmployeeCommand updateEmployeeCommand,
                               IDeleteEmployeeCommand deleteEmployeeCommand,
                               ICreateEmployeeClothesSizeCommand createEmployeeClothesSizeCommand,
                               IUpdateEmployeeClothesSizeCommand updateEmployeeClothesSizeCommand,
                               IDeleteEmployeeClothesSizeCommand deleteEmployeeClothesSizeCommand)
    {
        private readonly IGetAllEmployeesQuery _getAllEmployeesQuery = getAllEmployeesQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand = createEmployeeCommand;
        private readonly IUpdateEmployeeCommand _updateEmployeeCommand = updateEmployeeCommand;
        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand = deleteEmployeeCommand;
        private readonly ICreateEmployeeClothesSizeCommand _createEmployeeClothesSizeCommand = createEmployeeClothesSizeCommand;
        private readonly IUpdateEmployeeClothesSizeCommand _updateEmployeeClothesSizeCommand = updateEmployeeClothesSizeCommand;
        private readonly IDeleteEmployeeClothesSizeCommand _deleteEmployeeClothesSizeCommand = deleteEmployeeClothesSizeCommand;

        private readonly List<Employee> _employees = [];
        public IEnumerable<Employee> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<Employee> EmployeeAdded;
        public event Action<Employee> EmployeeUpdated;
        public event Action<Guid> EmployeeDeleted;

        public async Task Load()
        {
            try
            {
                IEnumerable<Employee> employee = await _getAllEmployeesQuery.Execute();

                _employees.Clear();

                if (employee != null)
                {
                    _employees.AddRange(employee);
                }

                EmployeesLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                //TODO: Fehlerbehandlung beim laden der aus DB
                Console.WriteLine($"Fehler beim Laden der Mitarbeiter: {ex.Message}");
            }
        }

        public async Task Add(Employee employee)
        {
            foreach (EmployeeClothesSize employeeClothesSize in employee.Clothes)
            {
                AddEmployeeClothesSize(employeeClothesSize);
            }

            //await _createEmployeeCommand.Execute(employee);
            _employees.Add(employee);
            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(Employee employee)
        {
            foreach (EmployeeClothesSize employeeClothesSize in employee.Clothes)
            {
                UpdateEmployeeClothesSize(employeeClothesSize);
            }

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

        public async Task Delete(Employee employee)
        {
            foreach (EmployeeClothesSize employeeClothesSize in employee.Clothes)
            {
                DeleteEmployeeClothesSize(employeeClothesSize.GuidID);
            }

            //await _deleteEmployeeCommand.Execute(guidID);
            _employees.RemoveAll(y => y.GuidID == employee.GuidID);
            EmployeeDeleted?.Invoke(employee.GuidID);
        }


        public async Task AddEmployeeClothesSize(EmployeeClothesSize employeeClothesSize)
        {
            //await _createEmployeeClothesSizeCommand.Execute(employeeClothesSize);
        }

        public async Task UpdateEmployeeClothesSize(EmployeeClothesSize employeeClothesSize)
        {
            //await _updateEmployeeClothesSizeCommand.Execute(employeeClothesSize);
        }

        public async Task DeleteEmployeeClothesSize(Guid guidID)
        {
            //await _deleteEmployeeClothesSizeCommand.Execute(guidID);
        }
    }
}
