using DVS.WPF.ViewModels;

namespace DVS.WPF.Stores
{
    public class SelectedDetailedEmployeeClothesItemStore
    {
        private DetailedEmployeeListingItemViewModel _selectedDetailedEmployeeItem;
        public DetailedEmployeeListingItemViewModel SelectedDetailedEmployeeItem
        {
            get => _selectedDetailedEmployeeItem;

            set
            {
                _selectedDetailedEmployeeItem = value;
                SelectedDetailedEmployeeItemChanged?.Invoke();
            }
        }

        public event Action? SelectedDetailedEmployeeItemChanged;
    }
}
