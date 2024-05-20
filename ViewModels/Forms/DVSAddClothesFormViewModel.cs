using DVS.Commands;
using DVS.Commands.AddClothesViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels.Forms
{
    internal class DVSAddClothesFormViewModel : ViewModelBase
    {
        public ICommand OpenAddEditCategoriesCommand {  get; }
        public ICommand CloseModalCommand {  get; }

        public DVSAddClothesFormViewModel(ICommand openAddEditCategoriesCommand, ICommand closeModalCommand)
        {
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            CloseModalCommand = closeModalCommand;
        }
    }
}
