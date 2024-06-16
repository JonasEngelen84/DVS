using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.EmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel,
                                    EmployeeStore employeeStore)
                                    : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEmployeeFormViewModel;

            addEmployeeFormViewModel.ErrorMessage = null;
            addEmployeeFormViewModel.IsSubmitting = true;

            EmployeeModel employee = new(
                addEmployeeFormViewModel.Id,
                addEmployeeFormViewModel.Firstname,
                addEmployeeFormViewModel.Lastname,
                addEmployeeFormViewModel.Comment);

            foreach(ClothesModel clothes in addEmployeeFormViewModel.AddEditEmployee_EmployeeClothesListviewViewModel.Employeeclothes)
            {
                //employee.Clothes.Add(clothes);
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
            }
        }
    }
}
