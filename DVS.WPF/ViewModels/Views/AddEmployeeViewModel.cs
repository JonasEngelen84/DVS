using DVS.WPF.Commands.AddEditEmployeeCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
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
