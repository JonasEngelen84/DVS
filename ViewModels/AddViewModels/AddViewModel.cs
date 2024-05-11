using DVS.Commands.AddViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels
{
    internal class AddViewModel : ViewModelBase
    {
        public ICommand EnterAddClothesCommand { get; }
        public ICommand CancelAddClothesCommand { get; }

        public AddViewModel(ModalNavigationStore _modalNavigationStore)
        {
            EnterAddClothesCommand = new EnterAddClothesCommand(this);
            CancelAddClothesCommand = new CancelAddClothesCommand(_modalNavigationStore);
        }
    }
}
