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

        private readonly List<SizeModel> _sizes =
            [
                new SizeModel(Guid.NewGuid(), "44", true),
                new SizeModel(Guid.NewGuid(), "46", true),
                new SizeModel(Guid.NewGuid(), "48", true),
                new SizeModel(Guid.NewGuid(), "50", true),
                new SizeModel(Guid.NewGuid(), "52", true),
                new SizeModel(Guid.NewGuid(), "54", true),
                new SizeModel(Guid.NewGuid(), "56", true),
                new SizeModel(Guid.NewGuid(), "58", true),
                new SizeModel(Guid.NewGuid(), "60", true),
                new SizeModel(Guid.NewGuid(), "62", true),
                new SizeModel(Guid.NewGuid(), "XS", false),
                new SizeModel(Guid.NewGuid(), "S", false),
                new SizeModel(Guid.NewGuid(), "M", false),
                new SizeModel(Guid.NewGuid(), "L", false),
                new SizeModel(Guid.NewGuid(), "XL", false),
                new SizeModel(Guid.NewGuid(), "XLL", false),
                new SizeModel(Guid.NewGuid(), "3XL", false),
                new SizeModel(Guid.NewGuid(), "4XL", false),
                new SizeModel(Guid.NewGuid(), "5XL", false),
                new SizeModel(Guid.NewGuid(), "6XL", false)
            ];
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

            //_sizes.Clear();

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
