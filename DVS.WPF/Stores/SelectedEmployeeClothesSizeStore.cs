using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class SelectedEmployeeClothesSizeStore
    {
        private EmployeeClothesSize _selectedEmployeeClothesSize;
        public EmployeeClothesSize SelectedEmployeeClothesSize
        {
            get => _selectedEmployeeClothesSize;

            set
            {
                if (_selectedEmployeeClothesSize != value)
                {
                    _selectedEmployeeClothesSize = value;
                }
            }
        }
    }
}
