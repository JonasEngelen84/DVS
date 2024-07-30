using DVS.Commands.AddEditEmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }
        public ICommand CloseAddEditEmployee { get; }


        public AddEmployeeViewModel(DVSListingViewModel dVSListingViewModel,
            EmployeeStore employeeStore, ClothesStore clothesStore,
            ModalNavigationStore modalNavigationStore)
        {
            ICommand addEmployee = new AddEmployeeCommand(this, employeeStore, modalNavigationStore);
            CloseAddEditEmployee = new CloseAddEditEmployeeCommand(clothesStore, modalNavigationStore);

            AddEditEmployeeFormViewModel = new(null, dVSListingViewModel, addEmployee)
            {
                ID = "ID",
                Lastname = "Nachname",
                Firstname = "Vorname",
                Comment = "Kommentar"
            };
        }
    }
}
