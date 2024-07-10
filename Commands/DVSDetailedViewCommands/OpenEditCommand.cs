using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly DVSListingViewModel _dVSDetailedEmployeesListingViewModel;
        private readonly DVSListingViewModel _dVSDetailedClothesListingViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;

        public OpenEditCommand(DVSListingViewModel dVSDetailedClothesListingViewModel,
                               DVSListingViewModel dVSDetailedEmployeesListingViewModel,
                               ModalNavigationStore modalNavigationStore,
                               CategoryStore categoryStore,
                               SeasonStore seasonStore,
                               SelectedCategoryStore selectedCategoryStore,
                               SelectedSeasonStore selectedSeasonStore,
                               SelectedClothesStore selectedClothesStore,
                               SelectedEmployeeClothesStore selectedEmployeeClothesStore,
                               ClothesStore clothesStore,
                               EmployeeStore employeeStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _dVSDetailedEmployeesListingViewModel = dVSDetailedEmployeesListingViewModel;
            _dVSDetailedClothesListingViewModel = dVSDetailedClothesListingViewModel;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _selectedCategoryStore = selectedCategoryStore;
            _selectedSeasonStore = selectedSeasonStore;
        }

        public override void Execute(object parameter)
        {
            
             if (_dVSDetailedEmployeesListingViewModel.SelectedDetailedEmployeeItem != null)
            {
                AddEditEmployeeViewModel addEditEmployeeViewModel = new(
                    _dVSDetailedEmployeesListingViewModel, _clothesStore, _employeeStore, _modalNavigationStore);

                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.ID =
                    _dVSDetailedEmployeesListingViewModel.SelectedDetailedEmployeeItem.ID;

                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.Lastname =
                    _dVSDetailedEmployeesListingViewModel.SelectedDetailedEmployeeItem.Lastname;
                
                addEditEmployeeViewModel.AddEditEmployeeFormViewModel.Firstname =
                    _dVSDetailedEmployeesListingViewModel.SelectedDetailedEmployeeItem.Firstname;

                //addEditEmployeeViewModel.AddEditEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection =
                //    _dVSListingViewModel.SelectedDetailedEmployeeItem.

                _dVSDetailedEmployeesListingViewModel.SelectedDetailedEmployeeItem = null;
                _modalNavigationStore.CurrentViewModel = addEditEmployeeViewModel;
            }
            else if (_dVSDetailedClothesListingViewModel.SelectedDetailedClothesItem != null)
            {
                AddEditClothesViewModel addEditClothesViewModel = new(
                    _modalNavigationStore, _categoryStore, _seasonStore,
                    _selectedCategoryStore, _selectedSeasonStore, _clothesStore);

                addEditClothesViewModel.AddEditClothesFormViewModel.ID =
                    _dVSDetailedClothesListingViewModel.SelectedDetailedClothesItem.ID;

                addEditClothesViewModel.AddEditClothesFormViewModel.Name =
                    _dVSDetailedClothesListingViewModel.SelectedDetailedClothesItem.Name;

                addEditClothesViewModel.AddEditClothesFormViewModel.Category =
                    _dVSDetailedClothesListingViewModel.SelectedDetailedClothesItem.Category;

                addEditClothesViewModel.AddEditClothesFormViewModel.Season =
                    _dVSDetailedClothesListingViewModel.SelectedDetailedClothesItem.Season;

                _dVSDetailedClothesListingViewModel.SelectedDetailedClothesItem = null;
                _modalNavigationStore.CurrentViewModel = addEditClothesViewModel;
            }
            else
            {
                string messageBoxText = $"Es wurde kein Objekt ausgwählt!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                _ = MessageBox.Show(messageBoxText, null, button, icon);
            }
        }
    }
}
