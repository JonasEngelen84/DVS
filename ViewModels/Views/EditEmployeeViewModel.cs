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
        private EmployeeModel Employee { get; }
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public EditEmployeeViewModel(EmployeeModel employee,
            DVSListingViewModel dVSListingViewModel, ModalNavigationStore modalNavigationStore)
        {
            Employee = employee;
            ICommand editEmployeeCommand = new EditEmployeeCommand(this, modalNavigationStore);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditEmployeeFormViewModel = new(
                employee, dVSListingViewModel, editEmployeeCommand)
            {
                ID = Employee.ID,
                Lastname = Employee.Lastname,
                Firstname = Employee.Firstname,
                Comment = Employee.Comment
            };
        }
    }
}
