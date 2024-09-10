using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class OpenAddClothesCommand(ModalNavigationStore modalNavigationStore,
                                       SizeStore sizeStore,
                                       CategoryStore categoryStore,
                                       SeasonStore seasonStore,
                                       ClothesStore clothesStore,
                                       ClothesSizeStore clothesSizeStore,
                                       EmployeeClothesSizesStore employeeClothesSizesStore,
                                       EmployeeStore employeeStore,
                                       DVSListingViewModel dVSListingViewModel)
                                       : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore,
                                                          _sizeStore,
                                                          _categoryStore,
                                                          _seasonStore,
                                                          _clothesStore,
                                                          _clothesSizeStore,
                                                          _employeeClothesSizesStore,
                                                          _employeeStore,
                                                          _dVSListingViewModel);

            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
