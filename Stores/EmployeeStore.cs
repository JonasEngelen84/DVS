using DVS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Stores
{
    public class EmployeeStore
    {
        private readonly ObservableCollection<EmployeeModel> _employees;
        public IEnumerable<EmployeeModel> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<EmployeeModel> EmployeeAdded;

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
    }
}
