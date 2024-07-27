using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands
{
    public class OpenEditDetailedItemCommand(SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                              SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore,
                              ModalNavigationStore modalNavigationStore, ClothesStore clothesStore,
                              EmployeeStore employeeStore, DVSListingViewModel dVSListingViewModel) : CommandBase
    {
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            if (_selectedDetailedClothesItemStore.SelectedDetailedClothesItem != null)
            {
                CommentClothesSizeViewModel commentClothesSizeViewModel = new(
                    _modalNavigationStore, _clothesStore, _selectedDetailedClothesItemStore);

                _modalNavigationStore.CurrentViewModel = commentClothesSizeViewModel;
            }
            else if (_selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem != null)
            {
                EditEmployeeViewModel EditEmployeeViewModel = new(selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem.Employee,
                    _employeeStore, _clothesStore, _modalNavigationStore, _dVSListingViewModel);

                _modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
            }
            else
            {
                string messageBoxText = "Es wurde kein Element ausgewählt!\nBitte erst das gewünschte Element auswählen.";
                string caption = "Kommentar bearbeiten";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                _ = MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
