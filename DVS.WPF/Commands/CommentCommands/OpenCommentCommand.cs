using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.CommentCommands
{
    public class OpenCommentCommand(SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
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
                    _modalNavigationStore, _clothesStore, _selectedDetailedClothesItemStore, _dVSListingViewModel);

                _modalNavigationStore.CurrentViewModel = commentClothesSizeViewModel;
            }
            else if (_selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem != null)
            {
                CommentEmployeeClothesViewModel commentEmployeeClothesViewModel = new(
                    _modalNavigationStore, _employeeStore, _selectedDetailedEmployeeClothesItemStore, _dVSListingViewModel);

                _modalNavigationStore.CurrentViewModel = commentEmployeeClothesViewModel;
            }
            else
            {
                string messageBoxText = "Bitte das gewünschte Element auswählen.";
                string caption = "Kommentieren";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                _ = MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
