using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class OpenAddEmployeeCommand(DVSListingViewModel dVSListingViewModel,
                                        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                        EmployeeStore employeeStore,
                                        ClothesStore clothesStore,
                                        SizeStore sizeStore,
                                        CategoryStore categoryStore,
                                        SeasonStore seasonStore,
                                        ClothesSizeStore clothesSizeStore,
                                        EmployeeClothesSizesStore employeeClothesSizesStore,
                                        ModalNavigationStore modalNavigationStore)
                                        : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _addEditEmployeeListingViewModel.LoadEmployeeClothes(null);
            _addEditEmployeeListingViewModel.LoadAvailableSizes();
            _addEditEmployeeListingViewModel.ClearEditedClothesList();

            AddEmployeeViewModel addEmployeeViewModel = new(_dVSListingViewModel,
                                                            _addEditEmployeeListingViewModel,
                                                            _employeeStore,
                                                            _clothesStore,
                                                            _sizeStore,
                                                            _categoryStore,
                                                            _seasonStore,
                                                            _clothesSizeStore,
                                                            _employeeClothesSizesStore,
                                                            _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
