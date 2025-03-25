using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands
{
    public class OpenEditEmployeeOrClothesCommand(
        SelectedClothesSizeStore selectedClothesSizeStore,
        SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore,
        ClothesStore clothesStore,
        EmployeeStore employeeStore,
        CategoryStore categoryStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        SeasonStore seasonStore,
        IDirtyEntitySaver dirtyEntitySaver)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (selectedClothesSizeStore.SelectedClothesSize != null)
            {
                EditClothesViewModel EditClothesViewModel = new(
                    selectedClothesSizeStore.SelectedClothesSize.Clothes,
                    modalNavigationStore,
                    categoryStore,
                    seasonStore,
                    clothesStore,
                    clothesSizeStore,
                    employeeClothesSizesStore,
                    employeeStore,
                    dirtyEntitySaver);

                modalNavigationStore.CurrentViewModel = EditClothesViewModel;
            }
            else if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
            {
                EditEmployeeViewModel EditEmployeeViewModel = new(
                    selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.Employee,
                    employeeStore,
                    clothesStore,
                    clothesSizeStore,
                    employeeClothesSizesStore,
                    modalNavigationStore);

                modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
            }
            else
                ShowErrorMessageBox("Bitte das gewünschte Element auswählen.", "Objekt Bearbeiten");
        }
    }
}
