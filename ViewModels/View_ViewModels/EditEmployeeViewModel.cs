using DVS.Commands;
using DVS.Commands.EmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public EditEmployeeFormViewModel EditEmployeeFormViewModel { get; }

        public EditEmployeeViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand EditEmployeeCommand = new EditEmployeeCommand(this, modalNavigationStore);
            ICommand cancelEmployeeCommand = new CloseModalCommand(modalNavigationStore);

            EditEmployeeFormViewModel = new EditEmployeeFormViewModel(EditEmployeeCommand, cancelEmployeeCommand);
        }
    }
}
