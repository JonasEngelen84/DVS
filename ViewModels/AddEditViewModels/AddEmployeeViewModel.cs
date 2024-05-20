using DVS.Commands;
using DVS.Commands.AddEmployeeViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddEditViewModels
{
    class AddEmployeeViewModel : ViewModelBase
    {
        public ICommand CloseModalCommand { get; }

        public AddEmployeeViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand EnterAddEmployeeCommand = new EnterAddEmployeeCommand(this);
            CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
        }
    }
}
