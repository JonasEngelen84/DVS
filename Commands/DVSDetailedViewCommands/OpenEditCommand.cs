using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;

        public OpenEditCommand(
            DVSListingViewModel dVSListingViewModel, ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore, SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore, ClothesStore clothesStore, EmployeeStore employeeStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _dVSListingViewModel = dVSListingViewModel;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _selectedCategoryStore = selectedCategoryStore;
            _selectedSeasonStore = selectedSeasonStore;
        }

        public override void Execute(object parameter)
        {
            if (_dVSListingViewModel.SelectedDetailedClothesItem != null)
            {
                AddEditClothesViewModel addEditClothesViewModel = new(
                    _modalNavigationStore, _categoryStore, _seasonStore,
                    _selectedCategoryStore, _selectedSeasonStore, _clothesStore);

                addEditClothesViewModel.AddEditClothesFormViewModel.ID =
                    _dVSListingViewModel.SelectedDetailedClothesItem.ID;

                addEditClothesViewModel.AddEditClothesFormViewModel.Name =
                    _dVSListingViewModel.SelectedDetailedClothesItem.Name;
                
                addEditClothesViewModel.AddEditClothesFormViewModel.Comment =
                    _dVSListingViewModel.SelectedDetailedClothesItem.Comment;
                
                addEditClothesViewModel.AddEditClothesFormViewModel.Category =
                    _dVSListingViewModel.SelectedDetailedClothesItem.Category;
                
                addEditClothesViewModel.AddEditClothesFormViewModel.Season =
                    _dVSListingViewModel.SelectedDetailedClothesItem.Season;

                _modalNavigationStore.CurrentViewModel = addEditClothesViewModel;
            }
            else if (_dVSListingViewModel.SelectedDetailedEmployeeItem != null)
            {
                AddEditEmployeeViewModel addEditEmployeeViewModel = new(
                    _dVSListingViewModel, _clothesStore, _employeeStore, _modalNavigationStore);

                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.ID =
                    _dVSListingViewModel.SelectedDetailedEmployeeItem.ID;

                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.Lastname =
                    _dVSListingViewModel.SelectedDetailedEmployeeItem.Lastname;
                
                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.Firstname =
                    _dVSListingViewModel.SelectedDetailedEmployeeItem.Firstname;
                
                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.Comment =
                    _dVSListingViewModel.SelectedDetailedEmployeeItem.Comment;
                
                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.Comment =
                    _dVSListingViewModel.SelectedDetailedEmployeeItem.Comment;

                //addEditEmployeeViewModel.AddEditEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection =
                //    _dVSListingViewModel.SelectedDetailedEmployeeItem.

                _modalNavigationStore.CurrentViewModel = addEditEmployeeViewModel;
            }
        }
    }
}
