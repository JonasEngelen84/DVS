using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class ClothesSizeStore(ICreateClothesSizeCommand createClothesSizeCommand,
                                  IUpdateClothesSizeCommand updateClothesSizeCommand,
                                  IDeleteClothesSizeCommand deleteClothesSizeCommand)
    {
        private readonly List<ClothesSize> _clothesSizes = [];
        public IEnumerable<ClothesSize> ClothesSizes => _clothesSizes;

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

        public async Task AddToDB(ClothesSize clothesSize)
        {
            await createClothesSizeCommand.Execute(clothesSize);

            _clothesSizes.Add(clothesSize);
        }
        
        public void AddToStore(ClothesSize clothesSize)
        {
            _clothesSizes.Add(clothesSize);
        }

        public async Task Update(ClothesSize editedClothesSize)
        {
            await updateClothesSizeCommand.Execute(editedClothesSize);

            int index = _clothesSizes.FindIndex(y => y.GuidId == editedClothesSize.GuidId);

            if (index != -1)
            {
                _clothesSizes[index] = editedClothesSize;
            }

            ClothesSizeUpdated.Invoke(editedClothesSize);
        }

        public async Task Delete(ClothesSize clothesSizeToDelete)
        {
            await deleteClothesSizeCommand.Execute(clothesSizeToDelete);

            int index = _clothesSizes.FindIndex(y => y.GuidId == clothesSizeToDelete.GuidId);

            if (index != -1)
            {
                _clothesSizes.RemoveAll(y => y.GuidId == clothesSizeToDelete.GuidId);
            }

            ClothesSizeDeleted.Invoke(clothesSizeToDelete);
        }
    }
}
