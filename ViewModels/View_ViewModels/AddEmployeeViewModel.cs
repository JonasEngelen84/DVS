using DVS.Commands;
using DVS.Commands.AddEmployeeViewCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }

        public AddEmployeeViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand SubmitAddEmployeeCommand = new SubmitAddEmployeeCommand(this, _modalNavigationStore);
            ICommand CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
            AddEditEmployeeFormViewModel = new AddEditEmployeeFormViewModel(SubmitAddEmployeeCommand, CloseModalCommand);
        }
    }
}
