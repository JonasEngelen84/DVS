using DVS.Commands;
using DVS.Commands.AddEditEmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public AddEmployeeViewModel(DVSListingViewModel dVSListingViewModel,
            EmployeeStore employeeStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand addEmployeeCommand = new AddEmployeeCommand(this, employeeStore, modalNavigationStore);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditEmployeeFormViewModel = new(null, dVSListingViewModel, addEmployeeCommand)
            {
                ID = "ID",
                Lastname = "Nachname",
                Firstname = "Vorname",
                Comment = "Kommentar"
            };
        }
    }
}
