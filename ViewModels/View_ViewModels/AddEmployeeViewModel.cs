using DVS.Commands;
using DVS.Commands.EmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }

        public AddEmployeeViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand submitAddEmployeeCommand = new SubmitAddEmployeeCommand(this, modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);
            AddEditEmployeeFormViewModel = new AddEditEmployeeFormViewModel(submitAddEmployeeCommand, closeModalCommand);
        }
    }
}
