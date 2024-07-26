using DVS.Models;

namespace DVS.Stores
{
    public class SelectedDetailedEmployeeClothesItemStore
    {
        private DetailedEmployeeListingItemModel _selectedDetailedEmployeeItem;
        public DetailedEmployeeListingItemModel SelectedDetailedEmployeeItem
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
