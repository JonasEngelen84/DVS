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
            _clothes =
            [
                new ClothesModel("111", "Winterhose", "Hose", "Winter", null)
                    { Sizes = [new ClothesSizeModel("S", 15, null), new ClothesSizeModel("M", 7, null), new ClothesSizeModel("L", 5, null), new ClothesSizeModel("XL", 2, null)] },

                new ClothesModel("112", "Sommershirt", "Shirt", "Sommer", null)
                    { Sizes = [new ClothesSizeModel("M", 5, null), new ClothesSizeModel("L", 6, null), new ClothesSizeModel("XL", 7, null)] },

                new ClothesModel("113", "Regenjacke", "Jacke", "Saisonlos", null)
                    { Sizes = [new ClothesSizeModel("S", 12, null), new ClothesSizeModel("M", 14, null), new ClothesSizeModel("L", 11, null), new ClothesSizeModel("XL", 8, null)] },

                new ClothesModel("114", "Wintershirt", "Shirt", "Winter", null)
                    { Sizes = [new ClothesSizeModel("S", 3, null), new ClothesSizeModel("M", 10, null), new ClothesSizeModel("L", 7, null), new ClothesSizeModel("XL", 6, null)] },

                new ClothesModel("115", "Schuhe", "Schuhwerk", "Saisonlos", null)
                    { Sizes = [new ClothesSizeModel("M", 11, null), new ClothesSizeModel("L", 10, null), new ClothesSizeModel("XL", 9, null)] }
            ];
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
