using DVS.Models;
using System.Collections.ObjectModel;

namespace DVS.Stores
{
    public class ClothesStore
    {
        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<ClothesModel> ClothesAdded;

        public ClothesStore()
        {
            _clothes = [new ClothesModel("111", "Sommershirt", "Shirt", "XL", "Sommer", 12, null),
                        new ClothesModel("112", "Sommershirt", "Shirt", "L", "Sommer", 8, null),
                        new ClothesModel("113", "Sommershirt", "Shirt", "M", "Sommer", 10, null),
                        new ClothesModel("114", "Wintershirt", "Shirt", "XL", "Winter", 8, null),
                        new ClothesModel("115", "Wintershirt", "Shirt", "L", "Winter", 5, null)];
        }

        public async Task Load()
        {
            ClothesLoaded?.Invoke();
        }

        public async Task Add(ClothesModel clothes)
        {
            _clothes.Add(clothes);
            ClothesAdded?.Invoke(clothes);
        }
    }
}
