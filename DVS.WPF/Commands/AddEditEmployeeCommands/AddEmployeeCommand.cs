using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel,
                                    EmployeeStore employeeStore,
                                    ClothesStore clothesStore,
                                    SizeStore sizeStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    ClothesSizeStore clothesSizeStore,
                                    EmployeeClothesSizesStore employeeClothesSizesStore,
                                    ModalNavigationStore modalNavigationStore)
                                    : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            addEmployeeFormViewModel.HasError = false;
            addEmployeeFormViewModel.IsSubmitting = true;

            Employee employee = new(Guid.NewGuid(),
                                    addEmployeeFormViewModel.ID,
                                    addEmployeeFormViewModel.Firstname,
                                    addEmployeeFormViewModel.Lastname,
                                    addEmployeeFormViewModel.Comment);

            if (addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList != null)
            {
                foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
                {
                    EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), employee, dclivm.ClothesSize, (int)dclivm.Quantity, "");
                    employee.Clothes.Add(employeeClothesSize);
                }
            }

            try
            {
                await _employeeStore.Add(employee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
                addEmployeeFormViewModel.HasError = true;
            }

            addEmployeeFormViewModel.IsSubmitting = false;

            _modalNavigationStore.Close();
        }
    }
}
