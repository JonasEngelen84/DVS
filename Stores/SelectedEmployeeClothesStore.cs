using DVS.Models;

namespace DVS.Stores
{
    public class SelectedEmployeeClothesStore
    {
        private EmployeeModel _selectedEmployeeClothes;
        public EmployeeModel SelectedEmployeeClothes
        {
            get => _selectedEmployeeClothes;
            set
            {
                _selectedEmployeeClothes = value;
                SelectedEmployeeClothesModelChanged?.Invoke();
            }
        }

        public event Action SelectedEmployeeClothesModelChanged;
    }
}
