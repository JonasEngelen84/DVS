using DVS.Commands;
using DVS.Commands.AddEmployeeViewCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.AddEditViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public DVSAddEmployeeFormViewModel DVSAddEmployeeFormViewModel { get; }

        public AddEmployeeViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand SubmitAddEmployeeCommand = new SubmitAddEmployeeCommand(this, _modalNavigationStore);
            ICommand CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
            DVSAddEmployeeFormViewModel = new DVSAddEmployeeFormViewModel(SubmitAddEmployeeCommand, CloseModalCommand);
        }
    }
}
