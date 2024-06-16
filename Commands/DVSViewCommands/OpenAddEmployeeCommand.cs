using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly DVSDetailedClothesListingViewModel _dVSDetailedClothesListingViewModel;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeCommand(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel,
                                      ClothesStore clothesStore,
                                      EmployeeStore employeeStore,
                                      ModalNavigationStore modalNavigationStore)
        {
            _dVSDetailedClothesListingViewModel = dVSDetailedClothesListingViewModel;
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(_dVSDetailedClothesListingViewModel,
                                                            _clothesStore,
                                                            _employeeStore,
                                                            _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
