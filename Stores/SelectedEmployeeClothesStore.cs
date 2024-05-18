using DVS.Models;

namespace DVS.Stores
{
    class SelectedEmployeeClothesStore
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
            }
        }
    }
}
