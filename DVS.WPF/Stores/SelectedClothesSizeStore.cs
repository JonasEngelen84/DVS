using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class SelectedClothesSizeStore
    {
        private ClothesSize _selectedClothesSize;
        public ClothesSize SelectedClothesSize
        {
            get =>_selectedClothesSize;

            set
            {
                _selectedClothesSize = value;
            }
        }
    }
}
