using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands
{
    public class OpenEditDetailedItemCommand(SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                                             SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore,
                                             ModalNavigationStore modalNavigationStore,
                                             AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                             SizeStore sizeStore,
                                             ClothesStore clothesStore,
                                             EmployeeStore employeeStore,
                                             DVSListingViewModel dVSListingViewModel,
                                             CategoryStore categoryStore,
                                             ClothesSizeStore clothesSizeStore,
                                             EmployeeClothesSizesStore employeeClothesSizesStore,
                                             SeasonStore seasonStore)
                                             : CommandBase
    {
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override void Execute(object parameter)
        {
            if (_selectedDetailedClothesItemStore.SelectedDetailedClothesItem != null)
            {
                EditClothesViewModel EditClothesViewModel = new(_selectedDetailedClothesItemStore.SelectedDetailedClothesItem.Clothes,
                                                                _modalNavigationStore,
                                                                _sizeStore,
                                                                _categoryStore,
                                                                _seasonStore,
                                                                _clothesStore,
                                                                _clothesSizeStore,
                                                                _employeeClothesSizesStore,
                                                                _employeeStore,
                                                                _dVSListingViewModel);

                _modalNavigationStore.CurrentViewModel = EditClothesViewModel;
            }
            else if (_selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem != null)
            {
                EditEmployeeViewModel EditEmployeeViewModel = new(_selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem.Employee,
                                                                  _employeeStore,
                                                                  _clothesStore,
                                                                  _modalNavigationStore,
                                                                  _dVSListingViewModel,
                                                                  _addEditEmployeeListingViewModel);

                _modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
            }
            else
            {
                string messageBoxText = "Bitte das gewünschte Element auswählen.";
                string caption = "Bearbeiten";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                _ = MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
