using DVS.Models;

namespace DVS.Stores
{
    class SelectedEmployeeClothesStore
    {
        private EmployeeClothesModel _selectedEmployeeClothesModel;

        public EmployeeClothesModel SelectedEmployeeClothesModel
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
