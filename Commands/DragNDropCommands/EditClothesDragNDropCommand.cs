
using DVS.Models;

namespace DVS.Commands.DragNDropCommands
{
    public class EditClothesDragNDropCommand(DetailedClothesListingItemModel item, int i) : AsyncCommandBase
    {
        private readonly DetailedClothesListingItemModel _item = item;
        private readonly int _i = i;

        public override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
