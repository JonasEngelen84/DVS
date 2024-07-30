using DVS.ViewModels;

namespace DVS.Stores
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
