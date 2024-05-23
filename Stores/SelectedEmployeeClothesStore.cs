using DVS.Models;

namespace DVS.Stores
{
    public class SelectedEmployeeClothesStore
    {
        private EmployeeModel _selectedEmployeeClothesModel;

        public EmployeeModel SelectedEmployeeClothesModel
        {
            get
            {
                return _selectedEmployeeClothesModel;
            }
            set
            {
                _selectedEmployeeClothesModel = value;
                SelectedEmployeeClothesModelChanged?.Invoke();
            }
        }

        public event Action SelectedEmployeeClothesModelChanged;
    }
}
