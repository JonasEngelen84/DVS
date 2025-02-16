using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands
{
    public class OpenEditEmployeeOrClothesCommand(
        SelectedClothesSizeStore selectedClothesSizeStore,
        SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore,
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        SizeStore sizeStore,
        ClothesStore clothesStore,
        EmployeeStore employeeStore,
        DVSListingViewModel dVSListingViewModel,
        CategoryStore categoryStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        SeasonStore seasonStore)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (selectedClothesSizeStore.SelectedClothesSize != null)
            {
                EditClothesViewModel EditClothesViewModel = new(
                    selectedClothesSizeStore.SelectedClothesSize.Clothes,
                    modalNavigationStore,
                    sizeStore,
                    categoryStore,
                    seasonStore,
                    clothesStore,
                    clothesSizeStore,
                    employeeClothesSizesStore,
                    employeeStore,
                    dVSListingViewModel);

                modalNavigationStore.CurrentViewModel = EditClothesViewModel;
            }
            else if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
            {
                EditEmployeeViewModel EditEmployeeViewModel = new(
                    selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.Employee,
                    employeeStore,
                    clothesStore,
                    sizeStore,
                    categoryStore,
                    seasonStore,
                    clothesSizeStore,
                    employeeClothesSizesStore,
                    modalNavigationStore,
                    addEditEmployeeListingViewModel);

                modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
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
