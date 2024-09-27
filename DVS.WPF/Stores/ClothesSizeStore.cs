using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using System.Windows;

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
            IEnumerable<ClothesSize> clothesSizes = [];

            try
            {
                clothesSizes = await _getAllClothesSizesQuery.Execute();
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der ClothesSizes von Datenbank ist fehlgeschlagen!", "ClothesSizesStore, Load", button, icon);
            }

            _clothesSizes.Clear();

            if (clothesSizes != null)
            {
                _clothesSizes.AddRange(clothesSizes);
            }
        }

        public async Task Add(ClothesSize clothesSize)
        {
            try
            {
                await _createClothesSizeCommand.Execute(clothesSize);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Hinzufügen der ClothesSize in Datenbank ist fehlgeschlagen!", "ClothesSizeStore, Add", button, icon);
            }

            _clothesSizes.Add(clothesSize);
        }

        public async Task Update(ClothesSize clothesSize)
        {
            try
            {
                //await _updateClothesSizeCommand.Execute(clothesSize);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten des ClothesSize in Datenbank ist fehlgeschlagen!", "ClothesSizesStore, Update", button, icon);
            }

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

        public async Task Delete(ClothesSize clothesSize)
        {
            try
            {
                await _deleteClothesSizeCommand.Execute(clothesSize);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Löschen der ClothesSize aus Datenbank ist fehlgeschlagen!", "ClothesSizesStore, Delete", button, icon);
            }

            _clothesSizes.RemoveAll(y => y.GuidID == clothesSize.GuidID);
        }
    }
}
