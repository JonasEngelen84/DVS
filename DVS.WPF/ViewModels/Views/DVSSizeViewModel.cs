using DVS.WPF.Commands;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.CommentCommands;
using DVS.WPF.Commands.EmployeeCommands;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class DVSSizeViewModel(DVSListingViewModel dVSListingViewModel,
                                  ModalNavigationStore modalNavigationStore,
                                  SizeStore sizeStore,
                                  CategoryStore categoryStore,
                                  SeasonStore seasonStore,
                                  ClothesStore clothesStore,
                                  ClothesSizeStore clothesSizeStore,
                                  EmployeeClothesSizeStore employeeClothesSizesStore,
                                  EmployeeStore employeeStore,
                                  SelectedClothesSizeStore selectedClothesSizeStore,
                                  SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
                                  AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
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

        public ICommand OpenComment { get; } = new OpenCommentCommand(
            selectedClothesSizeStore,
            selectedEmployeeClothesSizeStore,
            modalNavigationStore,
            clothesStore,
            categoryStore,
            seasonStore,
            employeeStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            dVSListingViewModel);

        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(
            modalNavigationStore,
            sizeStore,
            categoryStore,
            seasonStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            employeeStore,
            dVSListingViewModel);

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(
            addEditEmployeeListingViewModel,
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
            addEditEmployeeListingViewModel,
            sizeStore,
            clothesStore,
            employeeStore,
            dVSListingViewModel,
            categoryStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            seasonStore);
    }
}
