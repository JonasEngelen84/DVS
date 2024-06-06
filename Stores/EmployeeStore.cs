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
            _employees = [new EmployeeModel("1324", "Engelen", "Jonas", null),
                          new EmployeeModel("1212", "Molik", "Nadine", null),
                          new EmployeeModel("1112", "Yüksel", "Kemal", null),
                          new EmployeeModel("1213", "Oetken", "Markus", null),
                          new EmployeeModel("1231", "Nickol", "Daniel", null),
                          new EmployeeModel("1132", "Yüksel", "Irfan", null),
                          new EmployeeModel("1221", "Killen", "Stefan", null)];
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
