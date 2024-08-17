using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class OpenAddEmployeeCommand(DVSListingViewModel dVSListingViewModel,
                                        EmployeeStore employeeStore,
                                        ClothesStore clothesStore,
                                        ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(_dVSListingViewModel,
                                                            _employeeStore,
                                                            _clothesStore,
                                                            _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
