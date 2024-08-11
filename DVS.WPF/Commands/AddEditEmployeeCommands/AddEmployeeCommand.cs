using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, EmployeeStore employeeStore,
        ModalNavigationStore modalNavigationStore) : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            addEmployeeFormViewModel.ErrorMessage = null;
            addEmployeeFormViewModel.IsSubmitting = true;

            Employee employee = new(Guid.NewGuid(),
                                    addEmployeeFormViewModel.ID,
                                    addEmployeeFormViewModel.Firstname,
                                    addEmployeeFormViewModel.Lastname,
                                    addEmployeeFormViewModel.Comment);

            foreach (DetailedClothesListingItemViewModel item in addEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection)
            {
                item.ClothesSize.EmployeeClothesSizes.Add(new EmployeeClothesSize(Guid.NewGuid(), employee, item.ClothesSize, (int)item.Quantity));
                employee.Clothes.Add(new EmployeeClothesSize(Guid.NewGuid(), employee, item.ClothesSize, (int)item.Quantity));
            }

            try
            {
                await _employeeStore.Add(employee);
            }
            catch (Exception)
            {
                addEmployeeFormViewModel.ErrorMessage = "Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEmployeeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
