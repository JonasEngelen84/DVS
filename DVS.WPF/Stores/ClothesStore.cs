using DVS.Domain.Commands.Clothes;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class ClothesStore(IGetAllClothesQuery getAllClothesQuery,
                              ICreateClothesCommand CreateClothesCommand,
                              IUpdateClothesCommand UpdateClothesCommand,
                              IDeleteClothesCommand DeleteClothesCommand)
    {
        private readonly IGetAllClothesQuery _getAllClothesQuery = getAllClothesQuery;
        private readonly ICreateClothesCommand _createClothesCommand = CreateClothesCommand;
        private readonly IUpdateClothesCommand _updateClothesCommand = UpdateClothesCommand;
        private readonly IDeleteClothesCommand _deleteClothesCommand = DeleteClothesCommand;

        private readonly List<ClothesModel> _clothes = [];
        public IEnumerable<ClothesModel> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<ClothesModel> ClothesAdded;
        public event Action<ClothesModel> ClothesUpdated;
        public event Action<Guid> ClothesDeleted;

        public async Task Load()
        {
            //await _getAllClothesQuery.Execute();
            ClothesLoaded.Invoke();
        }

        public async Task Add(ClothesModel clothes)
        {
            //await _createClothesCommand.Execute(clothes);
            _clothes.Add(clothes);
            ClothesAdded.Invoke(clothes);
        }

        public async Task Update(ClothesModel clothes)
        {
            //await _updateClothesCommand.Execute(clothes);

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
            //await _deleteClothesCommand.Execute(guidID);

            _clothes.RemoveAll(y => y.GuidID == guidID);
            ClothesDeleted?.Invoke(guidID);
        }
        
        public async Task DragNDropUpdate(ClothesModel clothes)
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
    }
}
