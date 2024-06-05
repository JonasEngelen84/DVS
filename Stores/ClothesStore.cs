using DVS.Models;
using System.Collections.ObjectModel;

namespace DVS.Stores
{
    public class ClothesStore
    {
        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;

        public event Action CLothesLoaded;
        public event Action<ClothesModel> ClothesAdded;

        public ClothesStore()
        {
            _clothes = [new ClothesModel(111, "Sommershirt", "Shirt", "XL", "Sommer", 12),
                        new ClothesModel(112, "Sommershirt", "Shirt", "L", "Sommer", 8),
                        new ClothesModel(113, "Sommershirt", "Shirt", "M", "Sommer", 10),
                        new ClothesModel(114, "Wintershirt", "Shirt", "XL", "Winter", 8),
                        new ClothesModel(115, "Wintershirt", "Shirt", "L", "Winter", 5),
                        new ClothesModel(116, "Wintershirt", "Shirt", "M", "Winter", 15),
                        new ClothesModel(211, "Sommerhose", "Hose", "58", "Sommer", 6),
                        new ClothesModel(212, "Sommerhose", "Hose", "55", "Sommer", 3),
                        new ClothesModel(213, "Sommerhose", "Hose", "48", "Sommer", 11),
                        new ClothesModel(311, "Winterhose", "Hose", "58", "Winter", 6),
                        new ClothesModel(312, "Winterhose", "Hose", "55", "Winter", 10),
                        new ClothesModel(313, "Winterhose", "Hose", "48", "Winter", 3),
                        new ClothesModel(411, "Regenjacke", "Jacke", "XL", "Saisonlos", 12),
                        new ClothesModel(412, "Regenjacke", "Jacke", "L", "Saisonlos", 7),
                        new ClothesModel(413, "Regenjacke", "Jacke", "M", "Saisonlos", 59),
                        new ClothesModel(511, "Fleecejacke", "Jacke", "L", "Saisonlos", 7),
                        new ClothesModel(512, "Winterjacke", "Jacke", "XL", "Winter", 2),
                        new ClothesModel(611, "Sommerkappe", "Kopfbedeckung", "", "Saisonlos", 8),
                        new ClothesModel(612, "Winterkappe", "Kopfbedeckung", "", "Saisonlos", 4)
            ];
        }

        public async Task Load()
        {
            CLothesLoaded?.Invoke();
        }

        public async Task Add(ClothesModel clothes)
        {
            _clothes.Add(clothes);
            ClothesAdded?.Invoke(clothes);
        }
    }
}
