using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class ClothesStore
    {
        private readonly List<ClothesModel> _clothes = [];
        public IEnumerable<ClothesModel> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<ClothesModel> ClothesAdded;
        public event Action<ClothesModel> ClothesUpdated;
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
        }

        public async Task Delete(Guid guidID)
        {
            _clothes.RemoveAll(y => y.GuidID == guidID);
            ClothesDeleted?.Invoke(guidID);
        }
        
        public async Task DragNDropUpdate(ClothesModel clothes)
        {            
            ClothesUpdated.Invoke(clothes);
        }
    }
}
