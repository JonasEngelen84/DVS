using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class SelectedClothesSizeStore
    {
        private ClothesSize _selectedClothesSize;
        public ClothesSize SelectedClothesSize
        {
            get => _selectedClothesSize;

            set
            {
                if (_selectedClothesSize != value)
                {
                    _selectedClothesSize = value;
                }
            }
        }
    }
}
