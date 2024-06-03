using DVS.Commands;
using DVS.Commands.EmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public AddEmployeeFormViewModel AddEmployeeFormViewModel { get; }

        public AddEmployeeViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand addEmployeeCommand = new AddEmployeeCommand(this, modalNavigationStore);
            ICommand cancelEmployeeCommand = new CloseModalCommand(modalNavigationStore);

            AddEmployeeFormViewModel = new AddEmployeeFormViewModel(addEmployeeCommand, cancelEmployeeCommand);
        }
    }
}
