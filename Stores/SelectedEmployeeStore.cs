using DVS.Models;

namespace DVS.Stores
{
    public class SelectedEmployeeStore
    {
        private readonly EmployeeStore _employeeStore;

        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                SelectedEmployeeChanged?.Invoke();
            }
        }

        public event Action SelectedEmployeeChanged;

        public SelectedEmployeeStore(EmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated += EmployeeStore_EmployeeUpdated;
        }

        private void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            SelectedEmployee = employee;
        }

        private void EmployeeStore_EmployeeUpdated(EmployeeModel EmployeeModel)
        {
            if (EmployeeModel.Id == SelectedEmployee?.Id)
            {
                SelectedEmployee = EmployeeModel;
            }
        }
    }
}
