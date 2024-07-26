using DVS.Stores;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.CommentCommands
{
    public class OpenCommentCommand(SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                              SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore,
                              ModalNavigationStore modalNavigationStore, ClothesStore clothesStore, EmployeeStore employeeStore) : CommandBase
    {
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;

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
                CommentEmployeeClothesViewModel commentEmployeeClothesViewModel = new(
                    _modalNavigationStore, _employeeStore, _selectedDetailedEmployeeClothesItemStore);

                _modalNavigationStore.CurrentViewModel = commentEmployeeClothesViewModel;
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
