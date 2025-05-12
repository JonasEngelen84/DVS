using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    //TODO: IDirtyEntitySaver implementieren
    public class ClothesSizeStore(ICreateClothesSizeCommand createClothesSizeCommand,
                                  IDeleteClothesSizeCommand deleteClothesSizeCommand)
    {
        private readonly List<ClothesSize> _clothesSizes = [];
        public IEnumerable<ClothesSize> ClothesSizes => _clothesSizes;

        public event Action<ClothesSize> ClothesSizeAdded;
        public event Action<ClothesSize> ClothesSizeUpdated;
        public event Action<ClothesSize> ClothesSizeDeleted;

        public void Load(List<ClothesSize> clothesSizes)
        {
            _clothesSizes.Clear();

            if (clothesSizes != null)
            {
                _clothesSizes.AddRange(clothesSizes);
            }
        }

        public async Task AddDataBase(ClothesSize newClothesSize)
        {
            await createClothesSizeCommand.Execute(newClothesSize);
            _clothesSizes.Add(newClothesSize);
            ClothesSizeAdded.Invoke(newClothesSize);
        }
        
        public void AddStore(ClothesSize newClothesSize)
        {
            _clothesSizes.Add(newClothesSize);
        }

        public void Update(ClothesSize editedClothesSize)
        {
            int index = _clothesSizes.FindIndex(y => y.Id == editedClothesSize.Id);

            if (index != -1)
            {
                _clothesSizes[index] = editedClothesSize;
            }

            ClothesSizeUpdated.Invoke(editedClothesSize);

            editedClothesSize.IsDirty = true;
        }

        public async Task Delete(ClothesSize clothesSizeToDelete)
        {
            await deleteClothesSizeCommand.Execute(clothesSizeToDelete);

            int index = _clothesSizes.FindIndex(y => y.Id == clothesSizeToDelete.Id);

            if (index != -1)
            {
                _clothesSizes.RemoveAll(y => y.Id == clothesSizeToDelete.Id);
            }

            ClothesSizeDeleted.Invoke(clothesSizeToDelete);
        }
    }
}
