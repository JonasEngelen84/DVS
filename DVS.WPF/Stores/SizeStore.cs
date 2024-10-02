using DVS.Domain.Commands.SizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using System.Windows;

namespace DVS.WPF.Stores
{
    public class SizeStore(IGetAllSizesQuery getAllSizesQuery, IUpdateSizeCommand updateSizeCommand)
    {
        private readonly IGetAllSizesQuery _getAllSizesQuery = getAllSizesQuery;
        private readonly IUpdateSizeCommand _updateSizeCommand = updateSizeCommand;

        private readonly List<SizeModel> _sizes = [];
        public IEnumerable<SizeModel> Sizes => _sizes;

        public async Task Load()
        {
            IEnumerable<SizeModel> sizes = [];

            try
            {
                sizes = await _getAllSizesQuery.Execute();                
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der Sizes von Datenbank ist fehlgeschlagen!", "SizesStore, Load", button, icon);
            }

            _sizes.Clear();

            if (sizes != null)
            {
                _sizes.AddRange(sizes);
            }
        }

        public async Task Update(SizeModel size)
        {
            try
            {
                //await _updateSizeCommand.Execute(size);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten der Size in Datenbank ist fehlgeschlagen!", "SizesStore, Update", button, icon);
            }

            int index = _sizes.FindIndex(y => y.GuidID == size.GuidID);

            if (index != -1)
            {
                _sizes[index] = size;
            }
            else
            {
                _sizes.Add(size);
            }
        }
    }
}
