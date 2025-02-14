using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class ClothesSizeStore(ICreateClothesSizeCommand createClothesSizeCommand,
                                  IUpdateClothesSizeCommand updateClothesSizeCommand,
                                  IDeleteClothesSizeCommand deleteClothesSizeCommand)
    {
        private readonly ICreateClothesSizeCommand _createClothesSizeCommand = createClothesSizeCommand;
        private readonly IUpdateClothesSizeCommand _updateClothesSizeCommand = updateClothesSizeCommand;
        private readonly IDeleteClothesSizeCommand _deleteClothesSizeCommand = deleteClothesSizeCommand;

        private readonly List<ClothesSize> _clothesSizes = [];
        public IEnumerable<ClothesSize> ClothesSizes => _clothesSizes;

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
            await _createClothesSizeCommand.Execute(clothesSize);

            _clothesSizes.Add(clothesSize);
        }
        
        public void AddToStore(ClothesSize clothesSize)
        {
            _clothesSizes.Add(clothesSize);
        }

        public async Task Update(ClothesSize editedClothesSize)
        {
            await _updateClothesSizeCommand.Execute(editedClothesSize);

            int index = _clothesSizes.FindIndex(y => y.GuidId == editedClothesSize.GuidId);

            if (index != -1)
            {
                _clothesSizes[index] = editedClothesSize;
            }
            else
            {
                _clothesSizes.Add(editedClothesSize);
            }
        }

        public async Task Delete(ClothesSize clothesSize)
        {
            await _deleteClothesSizeCommand.Execute(clothesSize);

            int index = _clothesSizes.FindIndex(y => y.GuidId == clothesSize.GuidId);

            if (index != -1)
            {
                _clothesSizes.RemoveAll(y => y.GuidId == clothesSize.GuidId);
            }
            else
            {
                throw new InvalidOperationException("Entfernen der Bekleidungsgröße nicht möglich.");
            }
        }
    }
}
