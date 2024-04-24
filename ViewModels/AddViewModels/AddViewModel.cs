using DVS.Commands.AddViewCommands;
using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels
{
    internal class AddViewModel : ViewModelBase
    {
        public ICommand EnterAddClothesCommand { get; }
        public ICommand CancelAddClothesCommand { get; }

        public AddViewModel()
        {
            EnterAddClothesCommand = new EnterAddClothesCommand(this);
            CancelAddClothesCommand = new CancelAddClothesCommand(this);
        }
    }
}
