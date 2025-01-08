using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class ClothesStore(IGetAllClothesQuery getAllClothesQuery,
                              ICreateClothesCommand createClothesCommand,
                              IUpdateClothesCommand updateClothesCommand,
                              IDeleteClothesCommand deleteClothesCommand)
    {
        private readonly IGetAllClothesQuery _getAllClothesQuery = getAllClothesQuery;
        private readonly ICreateClothesCommand _createClothesCommand = createClothesCommand;
        private readonly IUpdateClothesCommand _updateClothesCommand = updateClothesCommand;
        private readonly IDeleteClothesCommand _deleteClothesCommand = deleteClothesCommand;

        private readonly List<Clothes> _clothes = [];
        public IEnumerable<Clothes> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<Clothes> ClothesAdded;
        public event Action<Clothes> ClothesUpdated;
        public event Action<Guid> ClothesDeleted;

        public async Task Load()
        {
            IEnumerable<Clothes> clothes = await _getAllClothesQuery.Execute();

            _clothes.Clear();

            if (clothes != null)
            {
                _clothes.AddRange(clothes);
            }

            ClothesLoaded?.Invoke();
        }

        public async Task Add(Clothes clothes)
        {
            await _createClothesCommand.Execute(clothes);

            _clothes.Add(clothes);

            ClothesAdded.Invoke(clothes);
        }

        public async Task Update(Clothes updatedClothes)
        {
            await _updateClothesCommand.Execute(updatedClothes);

            int index = _clothes.FindIndex(y => y.GuidId == updatedClothes.GuidId);

            if (index != -1)
            {
                _clothes[index] = updatedClothes;
            }
            else
            {
                _clothes.Add(updatedClothes);
            }

            ClothesUpdated.Invoke(updatedClothes);
        }

        public async Task Delete(Clothes clothes)
        {
            await _deleteClothesCommand.Execute(clothes);

            int index = _clothes.FindIndex(y => y.GuidId == clothes.GuidId);

            if (index != -1)
            {
                _clothes.RemoveAll(y => y.GuidId == clothes.GuidId);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Bekleidung nicht möglich.");
            }            

            ClothesDeleted?.Invoke(clothes.GuidId);
        }
    }
}
