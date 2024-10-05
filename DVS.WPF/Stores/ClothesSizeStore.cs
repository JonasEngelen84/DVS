using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class ClothesSizeStore(IGetAllClothesSizesQuery getallClotesSizesQuery,
                                  ICreateClothesSizeCommand createClothesSizeCommand,
                                  IUpdateClothesSizeCommand updateClothesSizeCommand,
                                  IDeleteClothesSizeCommand deleteClothesSizeCommand)
    {
        private readonly IGetAllClothesSizesQuery _getAllClothesSizesQuery = getallClotesSizesQuery;
        private readonly ICreateClothesSizeCommand _createClothesSizeCommand = createClothesSizeCommand;
        private readonly IUpdateClothesSizeCommand _updateClothesSizeCommand = updateClothesSizeCommand;
        private readonly IDeleteClothesSizeCommand _deleteClothesSizeCommand = deleteClothesSizeCommand;

        private readonly List<ClothesSize> _clothesSizes = [];
        public IEnumerable<ClothesSize> ClothesSizes => _clothesSizes;

        public async Task Load()
        {
            IEnumerable<ClothesSize> clothesSizes = await _getAllClothesSizesQuery.Execute();

            _clothesSizes.Clear();

            if (clothesSizes != null)
            {
                _clothesSizes.AddRange(clothesSizes);
            }
        }

        public async Task Add(ClothesSize clothesSize)
        {
            await _createClothesSizeCommand.Execute(clothesSize);

            _clothesSizes.Add(clothesSize);
        }

        public async Task Update(ClothesSize updatedClothesSize)
        {
            await _updateClothesSizeCommand.Execute(updatedClothesSize);

            int index = _clothesSizes.FindIndex(y => y.GuidID == updatedClothesSize.GuidID);

            if (index != -1)
            {
                _clothesSizes[index] = updatedClothesSize;
            }
            else
            {
                _clothesSizes.Add(updatedClothesSize);
            }
        }

        public async Task Delete(ClothesSize clothesSize)
        {
            await _deleteClothesSizeCommand.Execute(clothesSize);

            _clothesSizes.RemoveAll(y => y.GuidID == clothesSize.GuidID);
        }
    }
}
