using DVS.Commands;
using DVS.Commands.EmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }

        public EditEmployeeViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand submitEditEmployeeCommand = new SubmitEditEmployeeCommand(this, modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditEmployeeFormViewModel = new AddEditEmployeeFormViewModel(submitEditEmployeeCommand, closeModalCommand);
        }
    }
}
