using DVS.Domain.Services.Interfaces;
using DVS.WPF.Commands;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.EmployeeCommands;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class DVSSizeViewModel(
        DVSListingViewModel dVSListingViewModel,
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        SelectedClothesSizeStore selectedClothesSizeStore,
        SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
        IDirtyEntitySaver dirtyEntitySaver)
        : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenFilterClothesList { get; } = new OpenFilterClothesListCommand(modalNavigationStore);
        public ICommand OpenFilterEmployeeList { get; } = new OpenFilterEmployeeListCommand(modalNavigationStore);
        public ICommand Plus { get; } = new PlusCommand(
            selectedClothesSizeStore,
            selectedEmployeeClothesSizeStore,
            employeeStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore);
        public ICommand Minus { get; } = new MinusCommand(
            selectedClothesSizeStore,
            selectedEmployeeClothesSizeStore,
            employeeStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore);
        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(
            modalNavigationStore,
            categoryStore,
            seasonStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            employeeStore,
            dirtyEntitySaver);
        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(
            employeeStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            modalNavigationStore,
            dVSListingViewModel);
        public ICommand OpenEditDetailedItem { get; } = new OpenEditEmployeeOrClothesCommand(
            selectedClothesSizeStore,
            selectedEmployeeClothesSizeStore,
            modalNavigationStore,
            clothesStore,
            employeeStore,
            categoryStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            seasonStore,
            dirtyEntitySaver);
    }
}
