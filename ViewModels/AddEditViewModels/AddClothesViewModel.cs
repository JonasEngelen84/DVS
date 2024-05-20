using DVS.Commands;
using DVS.Commands.AddClothesViewCommands;
using DVS.Stores;
using System.Windows.Input;
using DVS.ViewModels.AddViewModels.Forms;

namespace DVS.ViewModels.AddEditViewModels
{
    class AddClothesViewModel : ViewModelBase
    {
        public DVSAddClothesFormViewModel DVSAddClothesViewModel { get; }

        public AddClothesViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand EnterAddClothesCommand = new EnterAddClothesCommand();
            ICommand CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
        }
    }
}
