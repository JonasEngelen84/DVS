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
                //new ClothesModel("111", "Winterhose", new("Hose"), new("Winter"), null)
                //{
                //    Sizes = [new ClothesSizeModel("S") {Quantity = 15},
                //             new ClothesSizeModel("M") {Quantity = 9 },
                //             new ClothesSizeModel("L") {Quantity = 11},
                //             new ClothesSizeModel("XL") {Quantity = 5}]
                //},

                //new ClothesModel("112", "Sommershirt", new("Shirt"), new("Sommer"), null)
                //{
                //    Sizes = [new ClothesSizeModel("S") {Quantity = 15},
                //             new ClothesSizeModel("M") {Quantity = 9 },
                //             new ClothesSizeModel("L") {Quantity = 11},
                //             new ClothesSizeModel("XL") { Quantity = 5 }]
                //},

                //new ClothesModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), null)
                //{
                //    Sizes = [new ClothesSizeModel("S") {Quantity = 15},
                //             new ClothesSizeModel("M") {Quantity = 9 },
                //             new ClothesSizeModel("L") {Quantity = 11},
                //             new ClothesSizeModel("XL") { Quantity = 5 }]
                //},

                //new ClothesModel("114", "Wintershirt", new("Shirt"), new("Winter"), null)
                //{
                //    Sizes = [new ClothesSizeModel("S") {Quantity = 15},
                //             new ClothesSizeModel("M") {Quantity = 9 },
                //             new ClothesSizeModel("L") {Quantity = 11},
                //             new ClothesSizeModel("XL") { Quantity = 5 }]
                //},

                //new ClothesModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), null)
                //{
                //    Sizes = [new ClothesSizeModel("S") {Quantity = 15},
                //             new ClothesSizeModel("M") {Quantity = 9 },
                //             new ClothesSizeModel("L") {Quantity = 11},
                //             new ClothesSizeModel("XL") { Quantity = 5 }]
                //}
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
