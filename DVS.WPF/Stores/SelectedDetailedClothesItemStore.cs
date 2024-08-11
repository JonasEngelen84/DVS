using DVS.WPF.ViewModels;

namespace DVS.WPF.Stores
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
            }
        }
    }
}
