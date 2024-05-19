using DVS.Commands.AddViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddEditViewModels
{
    class AddClothesViewModel : ViewModelBase
    {
        public ICommand CancelAddClothesCommand { get; }

        public AddClothesViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand EnterAddClothesCommand = new EnterAddClothesCommand(this);
            CancelAddClothesCommand = new CancelAddClothesCommand(_modalNavigationStore);
        }
    }
}
