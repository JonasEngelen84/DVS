using DVS.Domain.Commands.ClothesSize;
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
            try
            {
                IEnumerable<ClothesSize> clothesSizes = await _getAllClothesSizesQuery.Execute();

                _clothesSizes.Clear();

                if (clothesSizes != null)
                {
                    _clothesSizes.AddRange(clothesSizes);
                }
            }
            catch (Exception ex)
            {
                //TODO: Fehlerbehandlung beim laden der Bekleidungsgrößen aus DB
                Console.WriteLine($"Fehler beim Laden der ClothesSizes: {ex.Message}");
            }
        }

        public async Task Add(ClothesSize clothesSize)
        {
            //await _createClothesSizeCommand.Execute(clothesSize);
            _clothesSizes.Add(clothesSize);
        }

        public async Task Update(ClothesSize clothesSize)
        {
            //await _updateClothesSizeCommand.Execute(clothesSize);

            int index = _clothesSizes.FindIndex(y => y.GuidID == clothesSize.GuidID);

            if (index != -1)
            {
                _clothesSizes[index] = clothesSize;
            }
            else
            {
                _clothesSizes.Add(clothesSize);
            }
        }

        public async Task Delete(Guid guidID)
        {
            //await _deleteClothesSizeCommand.Execute(guidID);
            _clothesSizes.RemoveAll(y => y.GuidID == guidID);
        }
    }
}
