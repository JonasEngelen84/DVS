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
            _clothes = [new ClothesModel("111", "Sommershirt", "Shirt", "Sommer", null),
                        new ClothesModel("112", "Sommershirt", "Shirt", "Sommer", null),
                        new ClothesModel("113", "Sommershirt", "Shirt", "Sommer", null),
                        new ClothesModel("114", "Wintershirt", "Shirt", "Winter", null),
                        new ClothesModel("115", "Wintershirt", "Shirt", "Winter", null)];
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
