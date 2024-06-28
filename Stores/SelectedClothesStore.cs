using DVS.Models;

namespace DVS.Stores
{
    public class SelectedClothesStore
    {
        private ClothesModel _selectedClothesModel;
        public ClothesModel SelectedClothesModel
        {
            get => _selectedClothesModel;
            set
            {
                _selectedClothesModel = value;
                SelectedClothesModelChanged?.Invoke();
            }
        }

        public event Action SelectedClothesModelChanged;
    }
}
