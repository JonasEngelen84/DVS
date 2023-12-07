using System;
using DVS.Models;

namespace DVS.Stores
{
    public class SelectedClothesStore
    {
        private ClothesModel? _selectedClothes;

        public ClothesModel SelectedClothes
        {
            get => _selectedClothes;
            set
            {
                _selectedClothes = value;
                SelectedClothesChanged?.Invoke();

            }
        }

        public event Action SelectedClothesChanged;
    }
}
