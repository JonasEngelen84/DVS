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
