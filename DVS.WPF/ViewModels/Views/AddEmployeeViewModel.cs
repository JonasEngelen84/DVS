using DVS.WPF.Commands.EmployeeCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel;
        public AddEmployeeFormViewModel AddEmployeeFormViewModel { get; }
        public ICommand CloseAddEditEmployee { get; }


        public AddEmployeeViewModel(
            EmployeeStore employeeStore,
            ClothesStore clothesStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizeStore,
            ModalNavigationStore modalNavigationStore,
            DVSListingViewModel dVSListingViewModel)
        {
            _addEditEmployeeListingViewModel = new(null, clothesSizeStore);

            ICommand addEmployee = new AddEmployeeCommand(
                this,
                employeeStore,
                clothesStore,
                clothesSizeStore,
                modalNavigationStore);

            CloseAddEditEmployee = new CloseAddEditEmployeeCommand(clothesStore, modalNavigationStore);
            AddEmployeeFormViewModel = new(_addEditEmployeeListingViewModel, addEmployee);
        }
    }
}
