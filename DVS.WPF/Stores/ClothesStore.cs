using DVS.Domain.Commands.Clothes;
using DVS.Domain.Commands.ClothesSize;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class ClothesStore(IGetAllClothesQuery getAllClothesQuery,
                              ICreateClothesCommand createClothesCommand,
                              IUpdateClothesCommand updateClothesCommand,
                              IDeleteClothesCommand deleteClothesCommand,
                              ICreateClothesSizeCommand createClothesSizeCommand,
                              IUpdateClothesSizeCommand updateClothesSizeCommand,
                              IDeleteClothesSizeCommand deleteClothesSizeCommand)
    {
        private readonly IGetAllClothesQuery _getAllClothesQuery = getAllClothesQuery;
        private readonly ICreateClothesCommand _createClothesCommand = createClothesCommand;
        private readonly IUpdateClothesCommand _updateClothesCommand = updateClothesCommand;
        private readonly IDeleteClothesCommand _deleteClothesCommand = deleteClothesCommand;
        private readonly ICreateClothesSizeCommand _createClothesSizeCommand = createClothesSizeCommand;
        private readonly IUpdateClothesSizeCommand _updateClothesSizeCommand = updateClothesSizeCommand;
        private readonly IDeleteClothesSizeCommand _deleteClothesSizeCommand = deleteClothesSizeCommand;

        private readonly List<Clothes> _clothes = [];
        public IEnumerable<Clothes> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<Clothes> ClothesAdded;
        public event Action<Clothes> ClothesUpdated;
        public event Action<Guid> ClothesDeleted;

        public async Task Load()
        {
            try
            {
                IEnumerable<Clothes> clothes = await _getAllClothesQuery.Execute();

                _clothes.Clear();

                if (clothes != null)
                {
                    _clothes.AddRange(clothes);
                }

                ClothesLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                //TODO: Fehlerbehandlung beim laden der aus DB
                Console.WriteLine($"Fehler beim Laden der Clothes: {ex.Message}");
            }
        }

        public async Task Add(Clothes clothes)
        {
            foreach (var clothesSize in clothes.Sizes)
            {
                AddClothesSize(clothesSize);
            }

            //await _createClothesCommand.Execute(clothes);
            _clothes.Add(clothes);
            ClothesAdded.Invoke(clothes);
        }

        public async Task Update(Clothes clothes)
        {
            foreach (var clothesSize in clothes.Sizes)
            {
                UpdateClothesSize(clothesSize);
            }

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

        public async Task Delete(Clothes clothes)
        {
            foreach (var clothesSize in clothes.Sizes)
            {
                DeleteClothesSize(clothesSize.GuidID);
            }

            //await _deleteClothesCommand.Execute(clothes.GuidID);
            _clothes.RemoveAll(y => y.GuidID == clothes.GuidID);
            ClothesDeleted?.Invoke(clothes.GuidID);
        }


        public async Task AddClothesSize(ClothesSize clothesSize)
        {
            //await _createClothesSizeCommand.Execute(clothesSize);
        }

        public async Task UpdateClothesSize(ClothesSize clothesSize)
        {
            //await _updateClothesSizeCommand.Execute(clothesSize);
        }

        public async Task DeleteClothesSize(Guid guidID)
        {
            //await _deleteClothesSizeCommand.Execute(guidID);
        }
    }
}
