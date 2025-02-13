using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class OpenAddEmployeeCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                        EmployeeStore employeeStore,
                                        ClothesStore clothesStore,
                                        ClothesSizeStore clothesSizeStore,
                                        EmployeeClothesSizeStore employeeClothesSizeStore,
                                        ModalNavigationStore modalNavigationStore,
                                        DVSListingViewModel dVSListingViewModel)
                                        : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizeStore = employeeClothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _addEditEmployeeListingViewModel.LoadAvailableSizes();
            _addEditEmployeeListingViewModel.LoadEmployeeClothes(null);

            AddEmployeeViewModel addEmployeeViewModel = new(_addEditEmployeeListingViewModel,
                                                            _employeeStore,
                                                            _clothesStore,
                                                            _clothesSizeStore,
                                                            _employeeClothesSizeStore,
                                                            _modalNavigationStore,
                                                            dVSListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
