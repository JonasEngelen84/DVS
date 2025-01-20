using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel,
                                    EmployeeStore employeeStore,
                                    ClothesSizeStore clothesSizeStore,
                                    ModalNavigationStore modalNavigationStore)
                                    : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            addEmployeeFormViewModel.HasError = false;
            addEmployeeFormViewModel.IsSubmitting = true;

            Employee employee = new(Guid.NewGuid(),
                                    addEmployeeFormViewModel.Id,
                                    addEmployeeFormViewModel.Lastname,
                                    addEmployeeFormViewModel.Firstname,
                                    addEmployeeFormViewModel.Comment);

            if (addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList != null)
            {
                foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
                {
                    ClothesSize newClothesSize = new(Guid.NewGuid(),
                                                     dclivm.ClothesSize.Clothes,
                                                     dclivm.ClothesSize.Size,
                                                     dclivm.ClothesSize.Quantity,
                                                     dclivm.ClothesSize.Comment);

                    EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), employee, dclivm.ClothesSize, (int)dclivm.Quantity, "");
                    employee.Clothes.Add(employeeClothesSize);

                    //try
                    //{
                    //    await _clothesSizeStore.Update(dclivm.ClothesSize);
                    //}
                    //catch
                    //{

                    //}
                }
            }

            try
            {
                await _employeeStore.Add(employee);
            }
            catch
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiter ist fehlgeschlagen!", "AddEmployeeCommand");
                addEmployeeFormViewModel.HasError = true;
            }

            addEmployeeFormViewModel.IsSubmitting = false;

            _modalNavigationStore.Close();
        }
    }
}
