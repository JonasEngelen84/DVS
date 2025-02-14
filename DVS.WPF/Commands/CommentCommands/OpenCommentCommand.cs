using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CommentCommands
{
    public class OpenCommentCommand(SelectedClothesSizeStore selectedDetailedClothesItemStore,
                                    SelectedEmployeeClothesSizeStore selectedDetailedEmployeeClothesItemStore,
                                    ModalNavigationStore modalNavigationStore,
                                    ClothesStore clothesStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    EmployeeStore employeeStore,
                                    ClothesSizeStore clothesSizeStore,
                                    EmployeeClothesSizeStore employeeClothesSizesStore,
                                    DVSListingViewModel dVSListingViewModel)
                                    : CommandBase
    {
        private readonly SelectedClothesSizeStore _selectedClothesSizeStore = selectedDetailedClothesItemStore;
        private readonly SelectedEmployeeClothesSizeStore _selectedEmployeeClothesSizeStore = selectedDetailedEmployeeClothesItemStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            if (_selectedClothesSizeStore.SelectedClothesSize != null)
            {
                CommentClothesSizeViewModel commentClothesSizeViewModel = new(_modalNavigationStore,
                                                                              _clothesStore,
                                                                              _categoryStore,
                                                                              _seasonStore,
                                                                              _clothesSizeStore,
                                                                              _selectedClothesSizeStore,
                                                                              _dVSListingViewModel);

                _modalNavigationStore.CurrentViewModel = commentClothesSizeViewModel;
            }
            else if (_selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
            {
                CommentEmployeeClothesViewModel commentEmployeeClothesViewModel = new(_modalNavigationStore,
                                                                                      _employeeStore,
                                                                                      _employeeClothesSizesStore,
                                                                                      _selectedEmployeeClothesSizeStore,
                                                                                      _dVSListingViewModel);

                _modalNavigationStore.CurrentViewModel = commentEmployeeClothesViewModel;
            }
            else
            {
                ShowErrorMessageBox("Bitte das gewünschte Element auswählen.", "Kommentieren");
            }
        }
    }
}
