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
        public event Action<ClothesModel> ClothesEdited;


        public ClothesStore()
        {
            _clothes =
            [
                new ClothesModel(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null)
                {
                    Sizes = [new ClothesSizeModel(Guid.NewGuid(), "S") { Quantity = 15 },
                        new ClothesSizeModel(Guid.NewGuid(), "M") { Quantity = 9 },
                        new ClothesSizeModel(Guid.NewGuid(), "L") { Quantity = 11 },
                        new ClothesSizeModel(Guid.NewGuid(), "XL") { Quantity = 5 }]
                },

                new ClothesModel(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null)
                {
                    Sizes = [new ClothesSizeModel(Guid.NewGuid(), "S") { Quantity = 15 },
                        new ClothesSizeModel(Guid.NewGuid(), "M") { Quantity = 9 },
                        new ClothesSizeModel(Guid.NewGuid(), "L") { Quantity = 11 },
                        new ClothesSizeModel(Guid.NewGuid(), "XL") { Quantity = 5 }]
                },

                new ClothesModel(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null)
                {
                    Sizes = [new ClothesSizeModel(Guid.NewGuid(), "S") { Quantity = 15 },
                        new ClothesSizeModel(Guid.NewGuid(), "M") { Quantity = 9 },
                        new ClothesSizeModel(Guid.NewGuid(), "L") { Quantity = 11 },
                        new ClothesSizeModel(Guid.NewGuid(), "XL") { Quantity = 5 }]
                },

                new ClothesModel(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null)
                {
                    Sizes = [new ClothesSizeModel(Guid.NewGuid(), "S") { Quantity = 15 },
                        new ClothesSizeModel(Guid.NewGuid(), "M") { Quantity = 9 },
                        new ClothesSizeModel(Guid.NewGuid(), "L") { Quantity = 11 },
                        new ClothesSizeModel(Guid.NewGuid(), "XL") { Quantity = 5 }]
                },

                new ClothesModel(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null)
                {
                    Sizes = [new ClothesSizeModel(Guid.NewGuid(), "S") { Quantity = 15 },
                        new ClothesSizeModel(Guid.NewGuid(), "M") { Quantity = 9 },
                        new ClothesSizeModel(Guid.NewGuid(), "L") { Quantity = 11 },
                        new ClothesSizeModel(Guid.NewGuid(), "XL") { Quantity = 5 }]
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

        public async Task Edit(ClothesModel clothes)
        {
            //int i = _clothes.FirstOrDefault
        }
    }
}
