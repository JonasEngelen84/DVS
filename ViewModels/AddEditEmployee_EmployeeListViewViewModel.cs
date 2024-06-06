using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditEmployee_EmployeeListViewViewModel : ViewModelBase
    {
        private readonly ObservableCollection<EmployeeModel> _employees;
        public IEnumerable<EmployeeModel> Employees => _employees;

        private readonly EmployeeStore _employeeStore;


        public AddEditEmployee_EmployeeListViewViewModel(EmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
            _employees = [];
            LoadClothes();
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
        }


        protected override void Dispose()
        {
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;

            base.Dispose();
        }

        private void LoadClothes()
        {
            _employees.Clear();

            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                _employees.Add(employee);
            }
        }

        private void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            _employees.Add(employee);
        }
    }
}
