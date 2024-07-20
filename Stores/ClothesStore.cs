using DVS.Models;

namespace DVS.Stores
{
    public class ClothesStore
    {
        private readonly List<ClothesModel> _clothes = [];
        public IEnumerable<ClothesModel> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<ClothesModel> ClothesAdded;
        public event Action<ClothesModel> DetailedClothesItemAdded;
        public event Action<ClothesModel> ClothesUpdated;
        public event Action<ClothesModel> DetailedClothesItemUpdated;
        public event Action<Guid> ClothesDeleted;


        public ClothesStore()
        {
            //_clothes =
            //[
            //    new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null)
            //    {
            //        Sizes = [new ClothesSizeModel("S") { Quantity = 15, IsSelected = true },
            //            new ClothesSizeModel("M") { Quantity = 9, IsSelected = true },
            //            new ClothesSizeModel("L") { Quantity = 11, IsSelected = true },
            //            new ClothesSizeModel("XL") { Quantity = 5, IsSelected = true }]
            //    },

            //    new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null)
            //    {
            //        Sizes = [new ClothesSizeModel("S") { Quantity = 15, IsSelected = true },
            //            new ClothesSizeModel("M") { Quantity = 9, IsSelected = true },
            //            new ClothesSizeModel("L") { Quantity = 11, IsSelected = true },
            //            new ClothesSizeModel("XL") { Quantity = 5, IsSelected = true }]
            //    },

            //    new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null)
            //    {
            //        Sizes = [new ClothesSizeModel("S") { Quantity = 15, IsSelected = true },
            //            new ClothesSizeModel("M") { Quantity = 9, IsSelected = true },
            //            new ClothesSizeModel("L") { Quantity = 11, IsSelected = true },
            //            new ClothesSizeModel("XL") { Quantity = 5, IsSelected = true }]
            //    },

            //    new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null)
            //    {
            //        Sizes = [new ClothesSizeModel("S") { Quantity = 15, IsSelected = true },
            //            new ClothesSizeModel("M") { Quantity = 9, IsSelected = true },
            //            new ClothesSizeModel("L") { Quantity = 11, IsSelected = true },
            //            new ClothesSizeModel("XL") { Quantity = 5, IsSelected = true }]
            //    },

            //    new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null)
            //    {
            //        Sizes = [new ClothesSizeModel("S") { Quantity = 15, IsSelected = true },
            //            new ClothesSizeModel("M") { Quantity = 9, IsSelected = true },
            //            new ClothesSizeModel("L") { Quantity = 11, IsSelected = true },
            //            new ClothesSizeModel("XL") { Quantity = 5, IsSelected = true }]
            //    }
            //];
        }


        public async Task Load()
        {
            ClothesLoaded.Invoke();
        }

        public async Task Add(ClothesModel clothes)
        {
            _clothes.Add(clothes);
            ClothesAdded.Invoke(clothes);
            DetailedClothesItemAdded.Invoke(clothes);
        }

        public async Task Update(ClothesModel clothes)
        {
            int index = _clothes.FindIndex(y => y.GuidID == clothes.GuidID);

            if (index != -1)
            {
                _clothes[index] = clothes;
            }
            else
            {
                _clothes.Add(clothes);
            }

            ClothesUpdated.Invoke(clothes);
            DetailedClothesItemUpdated.Invoke(clothes);
        }

        public async Task Delete(Guid guidID)
        {
            _clothes.RemoveAll(y => y.GuidID == guidID);
            ClothesDeleted?.Invoke(guidID);
        }
    }
}
