using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeCommand(DVSListingViewModel dVSListingViewModel,
                                      ClothesStore clothesStore,
                                      EmployeeStore employeeStore,
                                      ModalNavigationStore modalNavigationStore)
        {
            _dVSListingViewModel = dVSListingViewModel;
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEditEmployeeViewModel addEmployeeViewModel = new(_dVSListingViewModel,
                                                                _clothesStore,
                                                                _employeeStore,
                                                                _modalNavigationStore);

            addEmployeeViewModel.AddEditEmployeeFormViewModel.ID = "ID";
            addEmployeeViewModel.AddEditEmployeeFormViewModel.Lastname = "Nachname";
            addEmployeeViewModel.AddEditEmployeeFormViewModel.Firstname = "Vorname";
            addEmployeeViewModel.AddEditEmployeeFormViewModel.Comment = "Kommentar";

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
