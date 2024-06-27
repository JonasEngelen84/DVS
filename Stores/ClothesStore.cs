using DVS.Models;
using DVS.ViewModels;
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
                {
                    Sizes = [new ClothesSizeModel { Size = "S", Quantity = 15, Comment = null },
                             new ClothesSizeModel { Size = "M", Quantity = 9, Comment = null },
                             new ClothesSizeModel { Size = "L", Quantity = 11, Comment = null },
                             new ClothesSizeModel { Size = "XL", Quantity = 5, Comment = null }]
                },

                new ClothesModel("112", "Sommershirt", "Shirt", "Sommer", null)
                {
                    Sizes = [new ClothesSizeModel { Size = "S", Quantity = 15, Comment = null },
                             new ClothesSizeModel { Size = "M", Quantity = 9, Comment = null },
                             new ClothesSizeModel { Size = "L", Quantity = 11, Comment = null },
                             new ClothesSizeModel { Size = "XL", Quantity = 5, Comment = null }]
                },

                new ClothesModel("113", "Regenjacke", "Jacke", "Saisonlos", null)
                {
                    Sizes = [new ClothesSizeModel { Size = "S", Quantity = 15, Comment = null },
                             new ClothesSizeModel { Size = "M", Quantity = 9, Comment = null },
                             new ClothesSizeModel { Size = "L", Quantity = 11, Comment = null },
                             new ClothesSizeModel { Size = "XL", Quantity = 5, Comment = null }]
                },

                new ClothesModel("114", "Wintershirt", "Shirt", "Winter", null)
                {
                    Sizes = [new ClothesSizeModel { Size = "S", Quantity = 15, Comment = null },
                             new ClothesSizeModel { Size = "M", Quantity = 9, Comment = null },
                             new ClothesSizeModel { Size = "L", Quantity = 11, Comment = null },
                             new ClothesSizeModel { Size = "XL", Quantity = 5, Comment = null }]
                },

                new ClothesModel("115", "Schuhe", "Schuhwerk", "Saisonlos", null)
                {
                    Sizes = [new ClothesSizeModel { Size = "44", Quantity = 15, Comment = null },
                             new ClothesSizeModel { Size = "46", Quantity = 9, Comment = null },
                             new ClothesSizeModel { Size = "48", Quantity = 11, Comment = null },
                             new ClothesSizeModel { Size = "50", Quantity = 5, Comment = null }]
                }
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
