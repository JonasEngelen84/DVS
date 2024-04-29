using DVS.Models;

namespace DVS.Stores
{
    class SelectedClothesStore
    {
        private ClothesModel _selectedClothesModel;

        public ClothesModel SelectedClothesModel
        {
            get
            {
                return _selectedClothesModel;
            }
            set
            {
                _selectedClothesModel = value;
            }
        }
    }
}
