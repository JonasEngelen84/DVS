using DVS.Commands;
using DVS.Commands.AddEditEmployeeCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public EditEmployeeViewModel(EmployeeModel employee, EmployeeStore employeeStore,
            ModalNavigationStore modalNavigationStore, DVSListingViewModel dVSListingViewModel)
        {
            ICommand editEmployeeCommand = new EditEmployeeCommand(this, employeeStore, modalNavigationStore, employee.GuidID);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditEmployeeFormViewModel = new(employee, dVSListingViewModel, editEmployeeCommand)
            {
                ID = employee.ID,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Comment = employee.Comment
            };
        }
    }
}
