using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly DVSDetailedClothesListingViewModel _dVSDetailedClothesListingViewModel;
        private readonly EmployeeStore _employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeCommand(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel,
                                      ClothesStore clothesStore,
                                      EmployeeStore employeeStore,
                                      ModalNavigationStore modalNavigationStore)
        {
            _dVSDetailedClothesListingViewModel = dVSDetailedClothesListingViewModel;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(_dVSDetailedClothesListingViewModel,
                                                            _employeeStore,
                                                            _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
