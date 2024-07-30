using DVS.Domain.Models;

namespace DVS.Stores
{
    public class EmployeeStore
    {
        private readonly List<EmployeeModel> _employees = [];
        public IEnumerable<EmployeeModel> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<EmployeeModel> EmployeeAdded;
        public event Action<EmployeeModel> EmployeeUpdated;
        public event Action<Guid> EmployeeDeleted;


        public EmployeeStore()
        {
            
        }


        public async Task Load()
        {
            EmployeesLoaded?.Invoke();
        }

        public async Task Add(EmployeeModel employee)
        {
            _employees.Add(employee);
            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(EmployeeModel employee)
        {
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
            _employees.RemoveAll(y => y.GuidID == guidID);
            EmployeeDeleted?.Invoke(guidID);
        }
    }
}
