using DVS.Commands.AddViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels
{
    class AddEmployeeViewModel : ViewModelBase
    {
        public ICommand CancelAddEmployeeCommand { get; }

        public AddEmployeeViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand EnterAddEmployeeCommand = new EnterAddEmployeeCommand(this);
            CancelAddEmployeeCommand = new CancelAddEmployeeCommand(_modalNavigationStore);
        }
    }
}
