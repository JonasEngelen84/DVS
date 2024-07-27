using DVS.ViewModels;

namespace DVS.Stores
{
    public class SelectedDetailedClothesItemStore
    {
        private DetailedClothesListingItemViewModel _selectedDetailedClothesItem;
        public DetailedClothesListingItemViewModel SelectedDetailedClothesItem
        {
            get =>_selectedDetailedClothesItem;

            set
            {
                _selectedDetailedClothesItem = value;
                SelectedDetailedClothesChanged?.Invoke();
            }
        }

        public event Action? SelectedDetailedClothesChanged;
    }
}
